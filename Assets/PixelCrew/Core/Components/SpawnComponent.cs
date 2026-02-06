using PixelCrew;
using UnityEngine;

namespace Assets.PixelCrew.Core.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _targetPrefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = Instantiate(_targetPrefab, _target.position, Quaternion.identity);
            instantiate.transform.localScale = transform.lossyScale;
        }

        public void SpawnClip(string animationName = null)
        {
            var instance = Instantiate(_targetPrefab, _target.position, Quaternion.identity);
            instance.transform.localScale = transform.lossyScale;

            if (!string.IsNullOrEmpty(animationName))
            {
                var anim = instance.GetComponent<SpriteAnimation>();
                if (anim != null)
                {
                    anim.SetClip(animationName);
                }
            }
        }
    }
}