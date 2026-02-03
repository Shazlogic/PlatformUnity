using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Gameplay.Potions
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimationPick : MonoBehaviour
    {
        [SerializeField] private int _frameRate;
        [SerializeField] private Sprite[] _sprites;

        public UnityAction OnComplete;
        private SpriteRenderer _renderer;
        private float _secondPerFrame;
        private int _currentSpriteIndex;
        private float _nextFrameTime;
        private bool _isPlaying = true;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _secondPerFrame = 1f / _frameRate;
            _nextFrameTime = Time.time + _secondPerFrame;
        }

        public void Play()
        {
            _isPlaying = true;
            _currentSpriteIndex = 0;
            _nextFrameTime = Time.time + _secondPerFrame;
        }

        private void Update()
        {
            if (!_isPlaying || _nextFrameTime > Time.time) return;

            if (_currentSpriteIndex >= _sprites.Length)
            {
                _isPlaying = false;
                OnComplete?.Invoke();
                return;
            }

            _renderer.sprite = _sprites[_currentSpriteIndex];
            _nextFrameTime += _secondPerFrame;
            _currentSpriteIndex++;
        }
    }
}
