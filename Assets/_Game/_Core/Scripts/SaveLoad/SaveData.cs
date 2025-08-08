
using System;


namespace SoloGames.SaveLoad
{
    [Serializable]
    public class SaveData
    {
        // Level
        public int LevelIndex = 0;

        // Max win threshold
        public CubeNumberType MaxWinCubeValue = CubeNumberType.none;
    }
}
