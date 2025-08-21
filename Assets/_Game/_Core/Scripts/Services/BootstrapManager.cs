using SoloGames.Patterns;
using SoloGames.Services;
using UnityEngine;


namespace SoloGames.Managers
{

    public class BootstrapManager : MonoBehaviour
    {
        [SerializeField] private Service[] _services;

        private void Awake()
        {
            foreach (Service m in _services)
            {
                ServiceLocator.Register(m);
                m.Init();
            }
        }

    }
}
