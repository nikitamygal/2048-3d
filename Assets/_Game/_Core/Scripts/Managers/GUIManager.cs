using System;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Tools;
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

    public class GUIManager : MMSingleton<GUIManager>
    {
        [SerializeField] private List<PanelItem> _panels = new List<PanelItem>();

        public UIPanel GetPanel(UIPanelType panelType)
        {
            return _panels.FirstOrDefault(panel => panel.PanelType == panelType).Panel;
        }

    }
}
