using UnityEngine;

namespace PixelCrew.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpImpulse;
        [SerializeField] private LayerCheck _groundCheck;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Animator _animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

            var isJumping = _direction.y > 0;
            var isGrounded = IsGrounded();
            
            if (isJumping)
            {
                if (IsGrounded() && _rigidbody.velocity.y <= 0)
                {
                    _rigidbody.AddForce(Vector2.up * _jumpImpulse, ForceMode2D.Impulse);
                }
            }
            else if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }

            _animator.SetFloat("vertical-velocity", _rigidbody.velocity.y);
            _animator.SetBool("is-running", _direction.x != 0);
            _animator.SetBool("is-ground", isGrounded);
        }

        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }

        public void SaySomething()
        {
            Debug.Log("SaySomething");
        }
    }
}