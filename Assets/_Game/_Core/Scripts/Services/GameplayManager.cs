using MoreMountains.Tools;
using SoloGames.Patterns;
using SoloGames.Services;


namespace SoloGames.Managers
{
    public enum GameStates
    {
        Start,
        WaitingForInput,
        ShootingCube,
        SpawningNext,
        Win,
        Lose
    }

    public class GameplayManager : Service
    {
        public MMStateMachine<GameStates> GameState;

        protected GUIManager _guiManager;

        void Awake()
        {
            GameState = new MMStateMachine<GameStates>(gameObject, true);
        }

        public override void Init()
        {
            _guiManager = ServiceLocator.Get<GUIManager>();
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
