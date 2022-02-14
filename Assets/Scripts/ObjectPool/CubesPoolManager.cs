using Events;
using UnityEngine.Pool;

namespace PoolManager
{
   public class CubesPoolManager : PoolManager<Cube>
   {
      public static CubesPoolManager instance;

      private void Awake()
      {
         pool = new ObjectPool<Cube>(CreateCube, OnTakeCubeFromPool, OnReturnCubeToPool);
         instance = this;
      }

      private void Start()
      {
         StartCoroutine(CoroutineFillPool(defaultSpawnAmount));
         EventsFactory.instance.OnActiveObjectsCountChanged.Invoke(CurrentActiveObjectsCount); // Updating the UI about default spawned Cubes
         EventsFactory.instance.OnActiveObjectsCountChanged.AddListener(OnActiveCubeCountChanged);
         EventsFactory.instance.OnCubeDestroyed.AddListener(OnCubeDestroyed);
      }

      internal void OnActiveCubeCountChanged(int count)
      {
         if (count > CurrentActiveObjectsCount)
            StartCoroutine(CoroutineFillPool(count));
         else
            DeactivateObjects(CurrentActiveObjectsCount - count);
      }
      
      private Cube CreateCube()
      {
         Cube cube = Instantiate(objectPrefab, objectsParent);
         cube.SetPool(pool);
         instantiatedObjects.Add(cube);
         return cube;
      }

      private void OnTakeCubeFromPool(Cube cube)
      {
         cube.gameObject.SetActive(true);
         cube.SpawnAtRandomPosition();
         cube.MoveToRandomPoint();
      }

      private void OnReturnCubeToPool(Cube cube)
      {
         cube.gameObject.SetActive(false);
         cube.StopAllMovement();
      }

      private void OnCubeDestroyed(Cube cube)
      {
         pool.Release(cube);
         activeObjects.Remove(cube);
      }
      
      private void OnDestroy()
      {
         EventsFactory.instance.OnActiveObjectsCountChanged.RemoveListener(OnActiveCubeCountChanged);
      }
   }
}