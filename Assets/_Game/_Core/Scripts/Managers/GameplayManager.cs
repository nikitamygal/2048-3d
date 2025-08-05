// using MoreMountains.Tools;
using SoloGames.Patterns;
using UnityEngine;

namespace SoloGames.Managers
{
    public enum GameStates { Start, LaunchStart, LaunchStop, Flight, Finish }

    public class GameplayManager : MonoSingleton<GameplayManager>
    {

        // public MMStateMachine<GameStates> GameState;

        protected override void Awake()
        {
            base.Awake();
            Initialization();
        }

        protected void Initialization()
        {
            // GameState = new MMStateMachine<GameStates>(gameObject, true);
        }

        protected void OnStartState()
        {
            
        }

        protected void OnLaunchStartState()
        {
            
        }
        
        protected void OnLaunchStopState()
        {
            
        }

        protected void OnFlightState()
        {
            
        }

        protected void OnFinishStateState()
        {

        }

        // private void OnStateChange()
        // {
        //     switch (GameState.CurrentState)
        //     {
        //         case GameStates.Start:
        //             OnStartState();
        //             break;
        //         case GameStates.LaunchStart:
        //             OnLaunchStartState();
        //             break;
        //         case GameStates.LaunchStop:
        //             OnLaunchStopState();
        //             break;
        //         case GameStates.Flight:
        //             OnFlightState();
        //             break;
        //         case GameStates.Finish:
        //             OnFinishStateState();
        //             break;
        //     }
        // }

        // private void OnEnable()
        // {
        //     GameState.OnStateChange += OnStateChange;
        // }

        // private void OnDisable()
        // {
        //     GameState.OnStateChange -= OnStateChange;
        // }

    }
}
