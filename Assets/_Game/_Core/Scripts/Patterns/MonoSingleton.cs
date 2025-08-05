using UnityEngine;

namespace SoloGames.Patterns
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    T component = FindAnyObjectByType<T>();
                    if (component == null)
                    {
                        GameObject newGO = new GameObject(typeof(T).ToString());
                        _instance = newGO.AddComponent<T>();
                    }
                    if (_instance == null)
                    { 
                        Debug.LogError($"Can't create instance of {typeof(T)}.");
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
