using UnityEngine;

namespace SoloGames.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/Cube Item", fileName = "Cube Item")]
    public class CubeItemSO : ScriptableObject
    {
        public CubeNumberType Number;
        public Material Mat;
        public ParticleSystem MergeVFX;

        public int GetNumber()
        {
            return (int)Number;
        }
    }
}
