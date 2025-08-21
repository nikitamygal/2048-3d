using SoloGames.Managers;
using SoloGames.Patterns;
using UnityEngine;


namespace SoloGames.Gameplay
{
    public class LosingLine : MonoBehaviour
    {
        private GameplayManager _gameplayManager;

        private void Awake()
        {
            _gameplayManager = ServiceLocator.Get<GameplayManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Cube otherCube = other.gameObject.GetComponent<Cube>();
            if (otherCube == null || otherCube.IsShootingCube) return;

            _gameplayManager.GameState.ChangeState(GameStates.Lose);
        }

    }
}
