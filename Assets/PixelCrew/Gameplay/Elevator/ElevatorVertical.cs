using UnityEngine;

namespace PixelCrew.Gameplay.Elevator
{
    public class ElevatorVertical : MonoBehaviour
    {
        [SerializeField] private float _pixelOffset = -100f;
        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _waitTime = 1f;

        private Rigidbody2D _rigidBodyTarget;
        private Vector3 _startPosition;
        private Vector3 _offsetPosition;
        private Vector3 _targetPosition;
        private bool _isWaiting = false;
        private float _waitTimer = 0f;

        private void Start()
        {
            _rigidBodyTarget = GetComponent<Rigidbody2D>();

            _startPosition = transform.position;

            float worldUnitsOffset = _pixelOffset / 100f;
            _offsetPosition = _startPosition + new Vector3(0, worldUnitsOffset, 0);
            _targetPosition = _offsetPosition;
        }

        private void FixedUpdate()
        {
            if (_isWaiting) return;

            Vector2 newPosition = Vector2.MoveTowards(
                _rigidBodyTarget.position,
                _targetPosition,
                _speed * Time.fixedDeltaTime
            );

            _rigidBodyTarget.MovePosition(newPosition);

            if (Vector2.Distance(_rigidBodyTarget.position, _targetPosition) < 0.01f)
            {
                _rigidBodyTarget.MovePosition(_targetPosition);
                StartWaiting();
            }
        }

        private void Update()
        {
            if (_isWaiting)
            {
                _waitTimer -= Time.deltaTime;
                if (_waitTimer <= 0)
                {
                    _isWaiting = false;
                    SwitchTarget();
                }
            }
        }

        private void SwitchTarget()
        {
            _targetPosition = _targetPosition == (Vector3)_offsetPosition
                ? _startPosition
                : _offsetPosition;
        }

        private void StartWaiting()
        {
            _isWaiting = true;
            _waitTimer = _waitTime;
        }
    }
}