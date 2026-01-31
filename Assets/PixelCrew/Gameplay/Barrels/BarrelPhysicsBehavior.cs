using UnityEngine;

namespace PixelCrew.Gameplay.Barrels
{
    public class BarrelPhysicsBehavior : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        [SerializeField] private float heroBounceForce = 6f;
        [SerializeField] private float collisionDampening = 0.2f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
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
