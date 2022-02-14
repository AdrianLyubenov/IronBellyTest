using System.Collections.Generic;
using System.Linq;
using PoolManager;
using UnityEngine;

    public class FindNearestNeighbour : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        void Update()
        {
            if (CubesPoolManager.instance.ActiveObjects.Count <= 1)
                return;
            else
                ClearPoints();

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, FindClosestCube(CubesPoolManager.instance.ActiveObjects).transform.position);
        }

        Cube FindClosestCube(List<Cube> targets)
        {
            Vector3 position = transform.position;
            return targets.OrderBy(o => (o.transform.position - position).sqrMagnitude)
                .ElementAt(1); // Returns second element because first element is always this gameObject
        }

        public void ClearPoints()
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
