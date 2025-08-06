using SoloGames.Managers;
using UnityEngine;


namespace SoloGames.Gameplay
{
    public class EndLine : MonoBehaviour
    {
        private GameplayManager _gameplayManager => GameplayManager.Instance;

        private void OnTriggerEnter(Collider other)
        {
            Cube otherCube = other.gameObject.GetComponent<Cube>();
            if (otherCube == null || otherCube.IsShootingCube) return;

            _gameplayManager.GameState.ChangeState(GameStates.Lose);
        }

    }
}
