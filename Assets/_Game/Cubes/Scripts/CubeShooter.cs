using MoreMountains.Tools;
using SoloGames.Managers;
using UnityEngine;

namespace SoloGames.Gameplay
{
    public class CubeShooter : MonoBehaviour
    {
        // protected InputManager _inputManager => InputManager.Instance;
        [SerializeField] private Transform spawnPoint;    // точка появления куба
        [SerializeField] private float moveStep = 1.5f;   // шаг влево/вправо
        [SerializeField] private float maxX = 3f;         // ограничение по X
        [SerializeField] private float shootForce = 10f;


        private void HandleSwipe(Vector2 direction)
        {
            if (direction.x > 0.5f)
                MoveRight();
            else if (direction.x < -0.5f)
                MoveLeft();
            else if (direction.y > 0.5f)
                ShootCube();
        }

        private void MoveRight()
        {
            Vector3 pos = spawnPoint.position;
            pos.x = Mathf.Clamp(pos.x + moveStep, -maxX, maxX);
            spawnPoint.position = pos;
        }

        private void MoveLeft()
        {
            Vector3 pos = spawnPoint.position;
            pos.x = Mathf.Clamp(pos.x - moveStep, -maxX, maxX);
            spawnPoint.position = pos;
        }

        private void ShootCube()
        {
            // Cube cube = cubePool.GetCube(currentValue);
            // cube.transform.position = spawnPoint.position;
            // cube.SetValue(currentValue);

            // Rigidbody rb = cube.GetComponent<Rigidbody>();
            // rb.linearVelocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;

            // rb.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
        }
        
        private void OnEnable()
        {
            InputManager.OnSwipe += HandleSwipe;
        }

        private void OnDisable()
        {
            InputManager.OnSwipe -= HandleSwipe;
        }
    }
}
