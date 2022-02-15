using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

    public class PoolManager<T> : MonoBehaviour where T : Object
    {
        public List<T> activeObjects = new List<T>();
        public List<T> ActiveObjects{ get { return activeObjects; } }
        public ObjectPool<T> pool;
        
        protected int CurrentActiveObjectsCount { get { return pool.CountActive; } }
        protected readonly List<T> instantiatedObjects = new List<T>();
        
        [SerializeField]
        protected int defaultSpawnAmount;
        [SerializeField]
        protected T objectPrefab;
        [SerializeField]
        protected Transform objectsParent;
        
        protected IEnumerator CoroutineFillPool(int count)
        {
            // Coroutine because of possible instantiation 
            yield return null;

            while (CurrentActiveObjectsCount < count)
            {
                pool.Get();
                yield return null;
            }

            OnPoolFillComplete();
        }

        protected void DrainPull(int count)
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
                    OnPoolDrainComplete();
                    return;
                }
            }
        }

        protected virtual void OnPoolFillComplete()
        {
            UpdateActiveObjects();
        }
        
        protected virtual void OnPoolDrainComplete()
        {
            UpdateActiveObjects();
        }

        private void UpdateActiveObjects()
        {
            activeObjects = instantiatedObjects.Where(genericObject => genericObject.GameObject().activeSelf).ToList();
        }
        
    }
