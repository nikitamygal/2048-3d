using UnityEngine;


namespace SoloGames.Services
{
    public abstract class Service : MonoBehaviour
    {
        protected bool _initialized = false;

        public virtual void Init()
        {
        }

        public virtual bool IsInitialized => _initialized;
    }
}
