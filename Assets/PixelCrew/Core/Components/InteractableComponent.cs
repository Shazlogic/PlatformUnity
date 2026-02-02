using UnityEngine;
using UnityEngine.Events;

namespace Assets.PixelCrew.Core.Components
{
    public class InteractableComponent: MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void Interact()
        {
            _action?.Invoke();
        }
    }
}
