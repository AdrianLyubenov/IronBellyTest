using UnityEngine;
using Containers.Score;
using Events;
using PoolManager;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField]
    private Cube cube;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            EventsFactory.instance.OnCubeDestroyed.Invoke(cube);
            EventsFactory.instance.OnScoreChanged.Invoke(ScoreContainer.Score++);
        }
    }
}
