using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SoloGames.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/Cube List", fileName = "Cube List")]
    public class CubeListSO : ScriptableObject
    {
        public List<CubeItemSO> List = new List<CubeItemSO>();

        public CubeItemSO GetCubeItem(int number)
        {
            return List.FirstOrDefault(cube => cube.GetNumber() == number);
        }
    }
}
