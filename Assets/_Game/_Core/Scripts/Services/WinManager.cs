using SoloGames.Configs;
using SoloGames.Patterns;
using SoloGames.SaveLoad;
using SoloGames.Services;
using UnityEngine;


namespace SoloGames.Managers
{
    public class WinManager : Service
    {
        [SerializeField] private CubeWinningThreshold _winningThreshold;

        protected GameplayManager _gameplayManager;
        protected SaveLoadSystem _saveSystem;
        protected CubeItemSO _nextItemThreshold = null;

        public override void Init()
        {
            _gameplayManager = ServiceLocator.Get<GameplayManager>();
            _saveSystem = ServiceLocator.Get<SaveLoadSystem>();
        }

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            if (_saveSystem.Data == null || _saveSystem.Data.MaxWinCubeValue == CubeNumberType.none)
            {
                _nextItemThreshold = _winningThreshold.GetFirstThreshold();
                SaveNewValue(_nextItemThreshold.Number);
            }
            else
            {
                _nextItemThreshold = _winningThreshold.GetNextThreshold(_saveSystem.Data.MaxWinCubeValue);
            }
        }

        private bool IsNewWinningThreshold(int number)
        {
            if (_nextItemThreshold == null) return false;
            return _nextItemThreshold.GetNumber() == number;
        }

        private void SaveNewValue(CubeNumberType numberType)
        {
            _saveSystem.Data.MaxWinCubeValue = numberType;
            _saveSystem.SaveData();
        }

        private void OnMergeCubes(CubeItemSO newCubeConfig)
        {
            if (newCubeConfig == null) return;

            if (IsNewWinningThreshold(newCubeConfig.GetNumber()))
            {
                SaveNewValue(_nextItemThreshold.Number);
                _gameplayManager.GameState.ChangeState(GameStates.Win);
            }
        }

        private void OnEnable()
        {
            MergeManager.OnMerge += OnMergeCubes;
        }

        private void OnDisable()
        {
            MergeManager.OnMerge -= OnMergeCubes;
        }
    
    }
}
