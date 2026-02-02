using UnityEngine;

namespace Assets.PixelCrew.Core.Components
{
    public class TeleportComponent: MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;

        public void Teleport(GameObject target)
        {
            target.transform.position = _destTransform.position;
        }
    }
}
