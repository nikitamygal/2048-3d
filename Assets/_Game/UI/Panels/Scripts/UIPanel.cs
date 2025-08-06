using System;
using UnityEngine;


namespace SoloGames.UI
{
    public class UIPanel : MonoBehaviour, IPanel
    {
        public event Action OnPanelOpen;
        public event Action OnPanelClose;

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
