using SoloGames.Configs;
using SoloGames.SaveLoad;
using UnityEngine;


namespace SoloGames.Managers
{
    public class WinManager : MonoBehaviour
    {
        [SerializeField] private CubeWinningThreshold _winningThreshold;

        protected GameplayManager _gameplayManager => GameplayManager.Instance;
        protected SaveLoadSystem _saveSystem => SaveLoadSystem.Instance;
        protected CubeItemSO _nextItemThreshold = null;

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
