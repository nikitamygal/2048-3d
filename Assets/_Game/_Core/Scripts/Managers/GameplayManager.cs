using MoreMountains.Tools;


namespace SoloGames.Managers
{
    public enum GameStates { Start, Win, Lose }

    public class GameplayManager : MMSingleton<GameplayManager>
    {

        public MMStateMachine<GameStates> GameState;

        protected override void Awake()
        {
            base.Awake();
            Initialization();
        }

        protected void Initialization()
        {
            GameState = new MMStateMachine<GameStates>(gameObject, true);
        }

        protected void OnStartState()
        {

        }

        protected void OnWinState()
        {

        }
        
        protected void OnLoseState()
        {
            
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
