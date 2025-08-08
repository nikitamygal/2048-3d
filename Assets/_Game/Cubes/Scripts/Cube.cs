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
        [SerializeField] private LayerMask _cubeLayer;
        [SerializeField] private LayerMask _collideLayer;
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
        public bool IsContolled { get; set; }

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

            // Jump randomly
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;
            _rigidbody.AddForce(randomDirection * _jumpForce, ForceMode.Impulse);

            // Rotate randomly
            Vector3 randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized * Random.Range(3f, 10f);
            _rigidbody.AddTorque(randomTorque, ForceMode.Impulse);
        }

        public void FreezeRotation(bool state)
        {
            if (_rigidbody == null) return;
            _rigidbody.constraints = state ? RigidbodyConstraints.FreezeRotationX |
                                             RigidbodyConstraints.FreezeRotationY |
                                             RigidbodyConstraints.FreezeRotationZ :
                                             RigidbodyConstraints.None;
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

        public void FinalizeCollide()
        {
            if (!IsShootingCube || IsContolled) return;

            IsShootingCube = false;
            FreezeRotation(false);
            ActivateTrail(false);
        }

        private bool IsCubeLayer(int layer)
        {
            return MMLayers.LayerInLayerMask(layer, _cubeLayer);
        }

        private bool IsCollideLayer(int layer)
        {
            return MMLayers.LayerInLayerMask(layer, _collideLayer);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isMerging) return;

            // Cubes
            if (IsCubeLayer(collision.gameObject.layer))
            {
                FinalizeCollide();

                Cube otherCube = collision.gameObject.GetComponent<Cube>();
                if (otherCube == null)
                {
                    // Play feedbacks
                    _collideFeedbacks?.PlayFeedbacks();
                    return;
                }
                
                // Try merge cubes
                _mergeManager.TryMerge(this, otherCube);
                return;
            }

            // Walls, borders
            if (IsCollideLayer(collision.gameObject.layer))
            {
                FinalizeCollide();

                // Play feedbacks
                _collideFeedbacks?.PlayFeedbacks();
            }
        }
        #endregion

        #region ENABLE/DISABLE
        private void OnEnable()
        {
            if (!IsShootingCube) return;
            _spawnFeedbacks?.PlayFeedbacks();
            ActivateTrail(true);
        }

        private void OnDisable()
        {
            FinalizeMerge();
            transform.rotation = Quaternion.identity;
        }
        #endregion

    }
}
