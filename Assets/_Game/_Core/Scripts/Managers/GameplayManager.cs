using MoreMountains.Tools;
using UnityEngine;


namespace SoloGames.Managers
{
    public enum GameStates {
        Start,
        WaitingForInput,
        ShootingCube,
        SpawningNext,
        Win,
        Lose
    }

    public class GameplayManager : MMSingleton<GameplayManager>
    {
        public MMStateMachine<GameStates> GameState;

        protected GUIManager _guiManager => GUIManager.Instance;

        protected override void Awake()
        {
            base.Awake();
            PreInitialization();
        }

        protected void PreInitialization()
        {
            GameState = new MMStateMachine<GameStates>(gameObject, true);
        }

        protected void OnWinState()
        {
            _guiManager?.WinPanel?.Show();
        }

        protected void OnLoseState()
        {
            _guiManager?.LosePanel.Show();
        }

        private void OnStateChange()
        {
            Debug.Log($"GameState: {GameState.CurrentState}");
            switch (GameState.CurrentState)
            {
                case GameStates.Win:
                    OnWinState();
                    break;
                case GameStates.Lose:
                    OnLoseState();
                    break;
            }
        }

        private void OnEnable()
        {
            GameState.OnStateChange += OnStateChange;
        }

        private void OnDisable()
        {
            GameState.OnStateChange -= OnStateChange;
        }

    }
}
