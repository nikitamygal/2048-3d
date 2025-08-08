using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using SoloGames.Configs;
using SoloGames.Gameplay;
using UnityEngine;


namespace SoloGames.Managers
{
    public class MergeManager : MMSingleton<MergeManager>
    {
        [SerializeField] private MMF_Player _mergeFeedbackPlayer;
        [SerializeField] private MMFeedbacks _mergeFeedbacks;

        public static event Action<CubeItemSO> OnMerge;

        protected SpawnManager _spawnManager => SpawnManager.Instance;
        protected float _mergeDelay = 0.1f;
        private readonly HashSet<Cube> _lockedCubes = new();

        public void TryMerge(Cube a, Cube b)
        {
            if (a == b || _lockedCubes.Contains(a) || _lockedCubes.Contains(b)) return;
            if (a.Config.Number != b.Config.Number) return;

            _lockedCubes.Add(a);
            _lockedCubes.Add(b);

            a.PrepareForMerge();
            b.PrepareForMerge();

            StartCoroutine(DelayedMerge(a, b, 0.1f));
        }

        private IEnumerator<WaitForSeconds> DelayedMerge(Cube a, Cube b, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (a == null || b == null) yield break;

            MergeCubes(a, b);

            _lockedCubes.Remove(a);
            _lockedCubes.Remove(b);
        }

        public void MergeCubes(Cube a, Cube b)
        {
            int newValue = a.Config.GetNumber() * 2;
            Cube newCube = _spawnManager.SpawnNewCube(newValue, b.transform.position);
            _spawnManager.DespawnPoolObject(a);
            _spawnManager.DespawnPoolObject(b);

            // Play merge feedbacks
            PlayMergeFeedbacks(newCube.transform.position);

            // Finilize merge - jump
            newCube.FinalizeMerge();
            newCube.Jump();

            OnMerge?.Invoke(newCube.Config);
        }

        private void AssignParticlesTarget(ParticleSystem particleSystem)
        {
            MMF_Feedback particlesFeedback = _mergeFeedbackPlayer.FeedbacksList.FirstOrDefault(f => f is MMF_ParticlesInstantiation);
            MMF_ParticlesInstantiation mmfParticles = particlesFeedback as MMF_ParticlesInstantiation;

            if (mmfParticles != null)
            {
                mmfParticles.ParticlesPrefab = particleSystem;
            }
        }

        private void PlayMergeFeedbacks(Vector3 position)
        {
            _mergeFeedbacks?.PlayFeedbacks(position);
        }

    }
}


