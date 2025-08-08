using UnityEngine;

namespace SoloGames.Configs
{
    [CreateAssetMenu(menuName = "Game/Configs/Level Settings", fileName = "Level Settings")]
    public class LevelSettingsSO : ScriptableObject
    {
        public string Name;
        public GameObject LevelPrefab;
    }
}
