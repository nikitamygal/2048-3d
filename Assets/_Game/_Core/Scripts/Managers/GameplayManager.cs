using MoreMountains.Tools;
using SoloGames.UI;


namespace SoloGames.Managers
{
    public enum GameStates { Start, Win, Lose }

    public class GameplayManager : MMSingleton<GameplayManager>
    {
        public MMStateMachine<GameStates> GameState;

        protected GUIManager _guiManager => GUIManager.Instance;
        protected UIPanel _hudPanel;
        protected UIPanel _winPanel;
        protected UIPanel _losePanel;

        protected override void Awake()
        {
            base.Awake();
            Initialization();
        }

        protected void Initialization()
        {
            GameState = new MMStateMachine<GameStates>(gameObject, true);
            _hudPanel = _guiManager.GetPanel(UIPanelType.HUD);
            _winPanel = _guiManager.GetPanel(UIPanelType.Win);
            _losePanel = _guiManager.GetPanel(UIPanelType.Lose);
        }

        protected void OnStartState()
        {

        }

        protected void OnWinState()
        {
            _winPanel?.Show();
        }
        
        protected void OnLoseState()
        {
            _losePanel?.Show();
        }

        private void OnStateChange()
        {
            switch (GameState.CurrentState)
            {
                case GameStates.Start:
                    OnStartState();
                    break;
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
