using Assets.PixelCrew.Gameplay.Potions;
using PixelCrew.Core.Components;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int _heal;
    [SerializeField] private SpriteAnimationPick _pickupAnimation;

    public void ApplyHeal(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();

        if (healthComponent != null)
        {
            healthComponent.Heal(_heal);
            PlayPickupAnimation();
        }
    }

    private void PlayPickupAnimation()
    {
        GetComponent<Collider2D>().enabled = false;

        if (_pickupAnimation != null)
        {
            _pickupAnimation.OnComplete += OnAnimationComplete;
            _pickupAnimation.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnAnimationComplete()
    {
        Destroy(gameObject);
    }
}