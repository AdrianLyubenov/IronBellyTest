using Events;
using UnityEngine.Pool;

public class BulletsPoolManager : PoolManager<Bullet>
{
    public static BulletsPoolManager instance;
   
    private void Awake()
    {
        pool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool);
        activeObjects = instantiatedObjects;
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(CoroutineFillPool(defaultSpawnAmount));
    }

    protected override void OnPoolFillComplete()
    {
        // We dont need active bullets when game starts
        foreach (var bullet in activeObjects)
        {
            if(bullet.gameObject.activeSelf)
                pool.Release(bullet);
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(objectPrefab, objectsParent);
        bullet.SetPool(pool);
        instantiatedObjects.Add(bullet);
        return bullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        bullet.StartDeactivateCountdown();
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.OnDeactivate();
    }
}
