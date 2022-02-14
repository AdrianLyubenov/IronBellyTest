using UnityEngine;

namespace Events
{
    public class EventsFactory : MonoBehaviour
    {
        public static EventsFactory instance;
        public UnityIntEvent OnActiveObjectsCountChanged;
        public UnityIntEvent OnScoreChanged;
        public UnityCubeEvent OnCubeDestroyed;

        private void Awake()
        {
            instance = this;
        }
    }
}

