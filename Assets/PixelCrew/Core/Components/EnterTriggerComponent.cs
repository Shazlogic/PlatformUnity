using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Core.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;

        private static int _totalScore;

        public static int TotalScore => _totalScore;

        public static void CoinsToDispose(int count)
        {
            if (count < _totalScore)
            {
                _totalScore -= count;
            }
            else
            {
                _totalScore = 0;
            }
        }

        public static void Reset()
        {
            _totalScore = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                string coinType = gameObject.tag;
                int points = 0;

                if (coinType == "CoinSilver")
                {
                    points = 1;
                    Debug.Log($"Серебрянная монетка! +1 очко. Всего: {_totalScore + points}");
                }
                else if (coinType == "CoinGold")
                {
                    points = 10;
                    Debug.Log($"Золотая монетка! +10 очков. Всего: {_totalScore + points}");
                }

                _totalScore += points;
                _action?.Invoke();
            }
        }
    }
}