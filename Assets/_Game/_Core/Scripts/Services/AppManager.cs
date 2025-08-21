using UnityEngine;


namespace SoloGames.Managers
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField] private int _targetFramerate = 60;

        private void Awake()
        {
            SetFramerate();
        }

        private void SetFramerate()
        {
            Application.targetFrameRate = _targetFramerate;
        }
    }
}
