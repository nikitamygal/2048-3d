using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using SoloGames.Configs;
using SoloGames.Managers;
using Tiny;
using TMPro;
using UnityEngine;


namespace SoloGames.Gameplay
{
    public class Cube : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private CubeItemSO _config;
        [SerializeField] private LayerMask _acceptableCollisionLayers;
        [SerializeField] private float _jumpForce = 5.0f;

        [Header("Bindings")]
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TextMeshPro[] _labels;
        [SerializeField] private Trail _trailFX;

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _spawnFeedbacks;
        [SerializeField] private MMFeedbacks _collideFeedbacks;

        public CubeItemSO Config => _config;
        public Rigidbody RB => _rigidbody;
        public bool IsShootingCube { get; set; }

        protected MergeManager _mergeManager => MergeManager.Instance;
        protected Rigidbody _rigidbody;
        private bool _isMerging = false;

        private void Awake()
        {
            PreInitialization();
        }

        protected void PreInitialization()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Init(CubeItemSO itemConfig, bool isShooting = false)
        {
            if (itemConfig == null) return;
            _config = itemConfig;
            IsShootingCube = isShooting;

            ActivateTrail(false);
            SetNumberLabels((int)Config.Number);
            SetMaterial(Config.Mat);
        }

        #region VIEW
        protected void SetNumberLabels(int number)
        {
            if (_labels.Length == 0) return;

            for (int i = 0; i < _labels.Length; i++)
            {
                _labels[i].text = number.ToString();
            }
        }

        protected void SetMaterial(Material material)
        {
            if (_meshRenderer == null) return;
            _meshRenderer.material = material;
        }

        public void ActivateTrail(bool state)
        {
            if (_trailFX == null) return;
            _trailFX.gameObject.SetActive(state);
        }
        #endregion

        #region PHYSICS
        public void Jump()
        {
            if (_rigidbody == null) return;
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        public void FreezeRotation(bool lockZ)
        {
            if (_rigidbody == null) return;
            _rigidbody.constraints = lockZ ? RigidbodyConstraints.FreezeRotationZ : RigidbodyConstraints.None;
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

            Jump();
        }

        public void FinalizeCollide()
        {
            IsShootingCube = false;
            FreezeRotation(false);
            ActivateTrail(false);
        }

        private bool IsCollideLayer(int layer)
        {
            return MMLayers.LayerInLayerMask(layer, _acceptableCollisionLayers);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (_isMerging) return;

            if (IsCollideLayer(collision.gameObject.layer))
            {
                if (IsShootingCube)
                {
                    FinalizeCollide();
                }
            }

            Cube otherCube = collision.gameObject.GetComponent<Cube>();
            if (otherCube == null)
            {
                _collideFeedbacks?.PlayFeedbacks();
            }
            else
            {
                // Try merge cubes
                _mergeManager.TryMerge(this, otherCube);
             }
        }
        #endregion

        private void OnEnable()
        {
            if (!IsShootingCube) return;
            _spawnFeedbacks?.PlayFeedbacks();
            ActivateTrail(true);
        }

    }
}
