using SoloGames.Configs;
using UnityEngine;


namespace SoloGames.Managers
{
    public class WinManager : MonoBehaviour
    {
        [SerializeField] private CubeWinningThreshold _winningThreshold;

        protected GameplayManager _gameplayManager => GameplayManager.Instance;

        private bool IsNewWinningThreshold(int number)
        {
            return false; //newCubeConfig.GetNumber();
        }

        private void OnMergeCubes(CubeItemSO newCubeConfig)
        {
            if (newCubeConfig == null) return;

            if (IsNewWinningThreshold(newCubeConfig.GetNumber()))
            {
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
