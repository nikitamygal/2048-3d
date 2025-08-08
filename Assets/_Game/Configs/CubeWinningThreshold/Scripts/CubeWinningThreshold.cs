using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace SoloGames.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/Cube Winning Threshold", fileName = "Cube Winning Threshold")]
    public class CubeWinningThreshold : ScriptableObject
    {
        public List<CubeItemSO> WinList = new List<CubeItemSO>();

        public CubeItemSO GetFirstThreshold()
        {
            return WinList.First();
        }

        public CubeItemSO GetCurrentThreshold(CubeNumberType cubeNumberType)
        {
            return WinList.FirstOrDefault(item => item.Number == cubeNumberType);
        }

        public int GetCurrentIndex(CubeNumberType cubeNumberType)
        {
            return WinList.FindIndex(item => item.Number == cubeNumberType);
        }

        public CubeItemSO GetNextThreshold(CubeNumberType cubeNumberType)
        {
            int currentIndex = GetCurrentIndex(cubeNumberType);
            currentIndex = currentIndex + 1;
            if (currentIndex >= WinList.Count)
            {
                currentIndex = 0;
            }
            return WinList[currentIndex];
        }
    }
}
