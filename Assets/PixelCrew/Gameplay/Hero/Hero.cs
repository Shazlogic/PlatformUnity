using Assets.PixelCrew.Core.Components;
using PixelCrew.Core.Components;
using PixelCrew.Gameplay.Hero.Components;
using UnityEngine;

namespace PixelCrew.Gameplay.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpImpulse;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private SpawnComponent _footStepParticle;
        [SerializeField] private SpawnComponent _footJumpParticle;
        [SerializeField] private ParticleSystem _hitParticles;

        private Collider2D[] _interactionResult = new Collider2D[1];
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Animator _animator;
        private bool _isGrounded;
        private bool _allowDoubleJump;
        private bool _didDoubleJump;
        private bool _wasGrounded;
        private bool _isJumping;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _wasGrounded = _isGrounded;
            _isGrounded = IsGrounded();

            if (!_wasGrounded && _isGrounded)
            {
                OnLanded();
            }
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
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();

            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);


            _animator.SetFloat(VerticalVelocity, _rigidbody.velocity.y);
            _animator.SetBool(IsRunning, _direction.x != 0);
            _animator.SetBool(IsGroundKey, _isGrounded);

            UpdateSpriteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _allowDoubleJump = true;
                _isJumping = false;
            }

            if (isJumpPressing)
            {
                _isJumping = true;
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;

            if (!isFalling) return yVelocity;

            if (_isGrounded)
            {
                yVelocity += _jumpImpulse;
                SpawnFootDust("jump");
            }
            else if (_allowDoubleJump)
            {
                yVelocity = _jumpImpulse;
                _allowDoubleJump = false;
                _didDoubleJump = true;
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
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

        public void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger(Hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);

            if (EnterTriggerComponent.TotalScore > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var countCountToDispose = Mathf.Min(EnterTriggerComponent.TotalScore, 5);
            EnterTriggerComponent.CoinsToDispose(countCountToDispose);

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = countCountToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        /// <summary>
        /// Метод вызывается при приземлении персонажа.
        /// </summary>
        private void OnLanded()
        {
            if (_didDoubleJump)
            {
                SpawnJumpDust("fall");
                _didDoubleJump = false;
            }
        }

        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position,
                _interactionRadius,
                _interactionResult,
                _interactionLayer
            );

            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>();

                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }

        public void SpawnJumpDust(string clipName)
        {
            _footJumpParticle.SpawnClip(clipName);
        }

        public void SpawnFootDust(string clipName)
        {
            _footStepParticle.SpawnClip(clipName);
        }
    }
}