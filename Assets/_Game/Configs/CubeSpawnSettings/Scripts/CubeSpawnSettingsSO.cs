using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

namespace SoloGames.Configs
{
    [Serializable]
    public class SpawnEntity
    {
        public CubeItemSO Item;

        [Range(0, 100)]
        public int Percentage;
    }

    [CreateAssetMenu(menuName = "Game/Configs/Cube Spawn Settings", fileName = "Cube Spawn Settings")]
    public class CubeSpawnSettingsSO : ScriptableObject
    {
        public List<SpawnEntity> Spawns = new List<SpawnEntity>();

        public CubeItemSO GetRandomCube()
        {
            if (Spawns.Count == 0) return null;
            float randomPercent = Random.Range(0f, 100);
            float cumulative = 0f;
            SpawnEntity spawnEntity = null;

            for (int i = 0; i < Spawns.Count; i++)
            {
                cumulative += Spawns[i].Percentage;
                if (randomPercent <= cumulative)
                {
                    spawnEntity = Spawns[i];
                    break;
                }
            }
            return spawnEntity.Item;
        }
    }
}
