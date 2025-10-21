using UnityEngine;

namespace PixelCrew.Platform
{
    public class BarrelPhysicsBehavior : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        public float heroBounceForce = 6f;
        public float collisionDampening = 0.2f;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contactCount == 0) return;

            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 contactPoint = collision.contacts[0].point;
                Vector2 direction = ((Vector2)transform.position - contactPoint).normalized;

                _rigidbody.AddForce(direction * heroBounceForce, ForceMode2D.Impulse);
            }
            else
            {
                _rigidbody.velocity *= collisionDampening;
            }
        }
    }
}