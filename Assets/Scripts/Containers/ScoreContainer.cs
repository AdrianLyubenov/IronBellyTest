using Events;
using UnityEngine;

namespace Containers.Score
{
    public class ScoreContainer : MonoBehaviour
    {
        public static int Score
        {
            set
            {
                score = value;
                EventsFactory.instance.OnScoreChanged.Invoke(score);
            }
            get => score;
        }
        private static int score;
    }
}