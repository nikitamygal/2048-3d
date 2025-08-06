using MoreMountains.Feedbacks;
using SoloGames.Configs;
using Tiny;
using UnityEngine;

namespace SoloGames.Gameplay
{
    public class Cube : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private CubeItemSO _config;

        [Header("Bindings")]
        [SerializeField] private Trail _trailFX;

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _spawnFeedbacks;
        [SerializeField] private MMFeedbacks _collideFeedbacks;
        [SerializeField] private MMFeedbacks _mergeFeedbacks;

        public CubeItemSO Config => _config;
        public bool IsShootingCube { get; protected set; }

        protected BoxCollider _boxCollider;
        private Rigidbody _rigidbody;
        private bool _isMerging = false;

        private void Awake()
        {
            Initialization();
        }

        protected void Initialization()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        #region TRAIL
        public void ActivateTrail(bool state)
        {
            if (_trailFX == null) return;
            _trailFX.gameObject.SetActive(state);
        }
        #endregion

        #region MERGE
        public void PrepareForMerge()
        {
            _isMerging = true;
            _rigidbody.isKinematic = true;
        }

        public void FinalizeMerge()
        {
            _isMerging = false;
            _rigidbody.isKinematic = false;
        }

        public void MergeWith(Cube other)
        {
            // int newValue = Config.Number * 2;
            // MergeManager.Instance.SpawnMergedCube(newValue, transform.position);

            // MergeManager.Instance.DestroyCube(this);
            // MergeManager.Instance.DestroyCube(other);

            _mergeFeedbacks?.PlayFeedbacks();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isMerging) return;

            Cube otherCube = collision.gameObject.GetComponent<Cube>();
            if (otherCube == null) return;

            // MergeManager.Instance.TryMerge(this, otherCube);
        }
        #endregion

    }
}
