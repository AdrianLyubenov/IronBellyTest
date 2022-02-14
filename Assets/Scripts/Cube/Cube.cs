using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

   public class Cube : MonoBehaviour
   {
      [SerializeField] private Vector3 moveOffset;
      [SerializeField] private FindNearestNeighbour findNearestNeighbour;

      private IObjectPool<Cube> pool;

      private Vector3 position { get { return transform.position; } }

      public void SetPool(IObjectPool<Cube> pool)
      {
         this.pool = pool;
      }

      public void SpawnAtRandomPosition()
      {
         transform.position = GetRandomPointWithinOffset(moveOffset);
      }

      public void MoveToRandomPoint()
      {
         StartCoroutine(CoroutineMoveToPoint(GetRandomPointWithinOffset(moveOffset), 1f));
      }

      private Vector3 GetRandomPointWithinOffset(Vector3 offset)
      {
         float randomX = Random.Range(position.x - offset.x, position.x + offset.x);
         float randomY = Random.Range(position.y - offset.y, position.y + offset.y);
         float randomZ = Random.Range(position.z - offset.z, position.z + offset.z);

         return new Vector3(randomX, randomY, randomZ);
      }

      public IEnumerator CoroutineMoveToPoint(Vector3 targetPosition, float speed)
      {
         while (transform.position != targetPosition)
         {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
         }

         MoveToRandomPoint();
      }

      public void StopAllMovement()
      {
         StopAllCoroutines();
         findNearestNeighbour.ClearPoints();
      }
   }
