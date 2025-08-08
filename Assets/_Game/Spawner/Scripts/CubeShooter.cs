using MoreMountains.Feedbacks;
using SoloGames.Configs;
using SoloGames.Managers;
using UnityEngine;

namespace SoloGames.Gameplay
{
    public class CubeShooter : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private CubeSpawnSettingsSO _cubeSpawnSettings;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _respawnDelay = 1.0f;
        [SerializeField] private float _moveSpeed = 50f;
        [SerializeField] private float _maxX = 3f;
        [SerializeField] private float _shootForce = 10f;

        [Header("Feedbacks")]
        [SerializeField] private MMFeedbacks _shootFeedbacks;

        protected Cube _shootingCube = null;
        protected Vector3 _cubePosition => _shootingCube.transform.position;
        protected GameplayManager _gameplayManager => GameplayManager.Instance;
        protected SpawnManager _spawnManager => SpawnManager.Instance;
        private bool _canShoot = true;

        private void Start()
        {
            // Spawn new Cube on SpawningNext state
            if (_gameplayManager?.GameState != null)
            {
                _gameplayManager.GameState.OnStateChange += OnGameStateChange;
            }
            StateSpawningNext();
        }

        private void OnInputTap(bool isUp)
        {
            if (!_canShoot || _shootingCube == null) return;

            StateShootingCube();

            if (isUp)
            {
                ShootCube();
                _canShoot = false;
                _shootingCube = null;
                Invoke(nameof(AllowShooting), _respawnDelay);
                Invoke(nameof(StateSpawningNext), _respawnDelay);
            }
        }

        private void AllowShooting()
        {
            _canShoot = true;
        }

        private void SpawnCube()
        {
            CubeItemSO randomCube = _cubeSpawnSettings.GetRandomCube();
            _shootingCube = _spawnManager.SpawnNewCube(randomCube, _spawnPoint.position);
            _shootingCube.FreezeRotation(true);
            _shootingCube.IsContolled = true;
        }

        private void ShootCube()
        {
            if (_shootingCube == null) return;

            _shootingCube.IsContolled = false;
            _shootingCube.RB.AddForce(Vector3.forward * _shootForce, ForceMode.Impulse);
            _shootFeedbacks?.PlayFeedbacks();
        }

        #region STATES
        private void StateShootingCube()
        {
            _gameplayManager.GameState.ChangeState(GameStates.ShootingCube);
        }

        private void StateSpawningNext()
        {
            _gameplayManager.GameState.ChangeState(GameStates.SpawningNext);
        }

        private void StateWaitingForInput()
        {
            _gameplayManager.GameState.ChangeState(GameStates.WaitingForInput);  
        }

        private void OnGameStateChange()
        {
            switch (_gameplayManager.GameState.CurrentState)
            {
                case GameStates.SpawningNext:
                    SpawnCube();
                    StateWaitingForInput();
                    break;
                case GameStates.Win:
                case GameStates.Lose:
                    gameObject.SetActive(false);
                    break;
            }
        }
        #endregion

        private void MoveCube()
        {
            if (_shootingCube == null || !InputManager.IsDragging) return;

            float targetX = Mathf.Clamp(InputManager.DragWorldPosition.x, -_maxX, _maxX);
            Vector3 current = _shootingCube.transform.position;
            Vector3 target = new Vector3(targetX, current.y, current.z);

            _shootingCube.transform.position = Vector3.Lerp(current, target, Time.deltaTime * _moveSpeed);
        }

        private void Update()
        {
            if (_gameplayManager.GameState.CurrentState == GameStates.WaitingForInput ||
                _gameplayManager.GameState.CurrentState == GameStates.ShootingCube)
            {
                MoveCube();
            }    
        }

        private void OnEnable()
        {
            InputManager.OnTap += OnInputTap;
        }

        private void OnDisable()
        {
            InputManager.OnTap -= OnInputTap;
            _gameplayManager.GameState.OnStateChange -= OnGameStateChange;
        }
        
    }
}
