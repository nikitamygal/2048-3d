using System;
using System.Collections.Generic;
using System.Linq;
using SoloGames.Patterns;
using SoloGames.UI;
using UnityEngine;

namespace SoloGames.Managers
{
    [Serializable]
    public class PanelItem
    {
        public UIPanelType PanelType;
        public UIPanel Panel;
    }

    public class GUIManager : MonoSingleton<GUIManager>
    {
        [SerializeField] private List<PanelItem> _panels = new List<PanelItem>();

        public PanelItem GetPanel(UIPanelType panelType)
        {
            return _panels.FirstOrDefault(panel => panel.PanelType == panelType);
        }
    }
}
