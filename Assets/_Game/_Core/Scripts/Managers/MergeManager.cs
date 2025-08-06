using System.Collections.Generic;
using MoreMountains.Tools;
using SoloGames.Configs;
using SoloGames.Gameplay;
using UnityEngine;


namespace SoloGames.Managers
{
    public class MergeManager : MMSingleton<MergeManager>
    {
        [SerializeField] private Cube cubePrefab;
        // [SerializeField] private CubePool cubePool;

        private readonly HashSet<Cube> lockedCubes = new();

        public void TryMerge(Cube a, Cube b)
        {
            if (a == b || lockedCubes.Contains(a) || lockedCubes.Contains(b)) return;
            if (a.Config.Number != b.Config.Number) return;

            lockedCubes.Add(a);
            lockedCubes.Add(b);

            a.PrepareForMerge();
            b.PrepareForMerge();

            StartCoroutine(DelayedMerge(a, b, 0.1f));
        }

        private IEnumerator<WaitForSeconds> DelayedMerge(Cube a, Cube b, float delay)
        {
            yield return new WaitForSeconds(delay);

            if (a == null || b == null) yield break;

            a.MergeWith(b);

            lockedCubes.Remove(a);
            lockedCubes.Remove(b);
        }

        public void SpawnMergedCube(int value, Vector3 position)
        {
            // Cube newCube = cubePool.GetCube(value);
            // newCube.transform.position = position + Vector3.up * 0.2f;
            // newCube.SetValue(value);

            // Rigidbody rb = newCube.GetComponent<Rigidbody>();
            // rb.linearVelocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;
        }

        // public void DestroyCube(Cube cube)
        // {
        //     cubePool.ReturnCube(cube);
        // }
    }
}


