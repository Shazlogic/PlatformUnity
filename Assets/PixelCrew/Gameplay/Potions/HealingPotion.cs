using PixelCrew.Core.Components;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int _heal;

    public void ApplyHeal(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();

        if (healthComponent != null)
        {
            healthComponent.Heal(_heal);
        }
    }
}