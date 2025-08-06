using MoreMountains.Feedbacks;
using SoloGames.Configs;
using UnityEngine;


namespace SoloGames.Gameplay
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private CubeItemSO _config;

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _spawnFeedbacks;
        [SerializeField] private MMFeedbacks _collideFeedbacks;
        [SerializeField] private MMFeedbacks _mergeFeedbacks;

        // public CubeItemSO Config { get; protected set; }
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

        private void OnCollisionEnter(Collision collision)
        {
            if (_isMerging) return;

            Cube otherCube = collision.gameObject.GetComponent<Cube>();
            if (otherCube == null) return;

            // MergeManager.Instance.TryMerge(this, otherCube);
        }

    }
}
