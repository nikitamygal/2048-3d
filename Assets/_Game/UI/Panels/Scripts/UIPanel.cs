using System;
using SoloGames.Patterns;
using SoloGames.SaveLoad;
using SoloGames.Services;
using UnityEngine;


namespace SoloGames.UI
{
    public class UIPanel : MonoBehaviour, IPanel
    {
        public event Action OnPanelOpen;
        public event Action OnPanelClose;

        protected SceneService _sceneLoader;
        protected SaveLoadSystem _saveSystem;

        protected virtual void Awake()
        {
            _sceneLoader = ServiceLocator.Get<SceneService>();
            _saveSystem = ServiceLocator.Get<SaveLoadSystem>();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnPanelOpen?.Invoke();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnPanelClose?.Invoke();
        }

    }
}
