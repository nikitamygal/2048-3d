using System.Collections.Generic;
using UnityEngine;

namespace SoloGames.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/Cube Settings", fileName = "Cube Settings")]
    public class CubeSettingsSO : ScriptableObject
    {
        public List<CubeItemSO> List = new List<CubeItemSO>();
    }
}
