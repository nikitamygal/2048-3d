using MoreMountains.Tools;
using SoloGames.Configs;
using SoloGames.Gameplay;
using UnityEngine;


namespace SoloGames.Managers
{
    public class SpawnManager : MMSingleton<SpawnManager>
    {
        [SerializeField] private CubeListSO _cubeList;
        [SerializeField] private Cube _cubePrefab;

        protected MMObjectPooler _pooler => MMObjectPooler.Instance;

        public Cube SpawnNewCube(int number, Vector3 position)
        {
            CubeItemSO config = _cubeList.GetCubeItem(number);
            return SpawnCube(config, position);
        }

        public Cube SpawnNewCube(CubeItemSO config, Vector3 position)
        {
            return SpawnCube(config, position);
        }

        public Cube SpawnCube(CubeItemSO config, Vector3 position)
        {
            GameObject newCubeObject = GetPoolObject();
            Cube newCube = newCubeObject.GetComponent<Cube>();
            newCube.transform.position = position;
            newCube.Init(config, true);
            newCube.FreezeRotation(true);
            newCube.gameObject.SetActive(true);

            return newCube;
        }

        public GameObject GetPoolObject()
        {
            return _pooler.GetPooledGameObject();
        }

        public void DespawnPoolObject(Cube cube)
        {
            cube.gameObject.SetActive(false);
        }
    }
}
