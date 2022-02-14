using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

    public class PoolManager<T> : MonoBehaviour where T : Object
    {
        public int CurrentActiveObjectsCount { get { return pool.CountActive; } }
        public List<T> ActiveObjects{ get { return activeObjects; } }
        internal ObjectPool<T> pool;
        
        internal readonly List<T> instantiatedObjects = new List<T>();
        internal List<T> activeObjects = new List<T>();
        
        [SerializeField]
        internal int defaultSpawnAmount;
        [SerializeField]
        internal T objectPrefab;
        [SerializeField]
        internal Transform objectsParent;
        
        internal IEnumerator CoroutineFillPool(int count)
        {
            yield return null;

            while (CurrentActiveObjectsCount < count)
            {
                pool.Get();
                yield return null;
            }

            OnPoolFillComplete();
        }

        internal void DeactivateObjects(int count)
        {
            int realesdCubes = 0;
            foreach (var cube in activeObjects)
            {
                if(!cube.GameObject().activeSelf)
                    continue;
                
                pool.Release(cube);
                realesdCubes++;

                if (realesdCubes == count)
                {
                    OnPoolClearComplete();
                    return;
                }
            }
        }

        protected virtual void OnPoolFillComplete()
        {
            activeObjects = instantiatedObjects.Where(x => x.GameObject().activeSelf).ToList();
        }
        
        protected virtual void OnPoolClearComplete()
        {
            activeObjects = instantiatedObjects.Where(x => x.GameObject().activeSelf).ToList();
        }
    }
