using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

    public class Bullet : MonoBehaviour
    {
        public Vector3 SpawnPoint
        {
            set { spawnPoint = value; }
        }

        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Vector3 shootForce;
        [SerializeField] private float deactivationCountdown;
        private IObjectPool<Bullet> pool;
        private Vector3 spawnPoint = Vector3.zero;

        public void SetPool(IObjectPool<Bullet> pool)
        {
            this.pool = pool;
        }

        public void MoveToSpawnPosition()
        {
            transform.position = spawnPoint;
        }

        public void AddForce()
        {
            rigidbody.AddForce(shootForce);
        }

        public void StartDeactivateCountdown()
        {
            StartCoroutine(CoroutineDeactivate());
        }

        private IEnumerator CoroutineDeactivate()
        {
            yield return new WaitForSeconds(deactivationCountdown);
            pool.Release(this);
        }

        public void OnDeactivate()
        {
            StopCoroutine(CoroutineDeactivate());
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
