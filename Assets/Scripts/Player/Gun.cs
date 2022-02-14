using PoolManager;
using UnityEngine;

namespace Player.Gun
{
    public class Gun : MonoBehaviour
    {
        [SerializeField]
        private Transform bulletSpawnPoint;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Bullet bullet = BulletsPoolManager.instance.pool.Get();
                bullet.SpawnPoint = bulletSpawnPoint.position;
                bullet.MoveToSpawnPosition();
                bullet.AddForce();
            }
        }
    }
}
