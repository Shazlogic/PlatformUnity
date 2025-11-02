using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action;
        public static int TotalScore { get; set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                string coinType = gameObject.tag;
                int points = 0;

                if (coinType == "CoinSilver")
                {
                    points = 1;
                    Debug.Log($"Серебрянная монетка! +1 очко. Всего: {TotalScore + points}");
                }
                else if (coinType == "CoinGold")
                {
                    points = 10;
                    Debug.Log($"Золотая монетка! +10 очков. Всего: {TotalScore + points}");
                }

                TotalScore += points;
                _action?.Invoke();
            }
        }
    }
}