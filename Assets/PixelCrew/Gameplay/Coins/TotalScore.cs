using UnityEngine;

namespace Assets.PixelCrew.Gameplay.Coins
{
    public class TotalScore : MonoBehaviour
    {
        [SerializeField] private GameObject _target;
        [SerializeField] private int _points;

        private static int _totalScore;

        public static int CurrentTotal => _totalScore;

        public void AddCoins()
        {
            string coinType = _target.tag;

            if (coinType == "CoinSilver")
            {
                Debug.Log($"Серебрянная монетка! Всего очков: {_totalScore + _points}");
            }
            else if (coinType == "CoinGold")
            {
                Debug.Log($"Золотая монетка! Всего очков: {_totalScore + _points}");
            }

            _totalScore += _points;
        }

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
    }
}
