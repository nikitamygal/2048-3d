using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoloGames.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/Cube Winning Threshold", fileName = "Cube Winning Threshold")]
    public class CubeWinningThreshold : ScriptableObject
    {
        public List<CubeItemSO> WinList = new List<CubeItemSO>();
    }
}
