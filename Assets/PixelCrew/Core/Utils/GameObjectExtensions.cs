using UnityEngine;

namespace Assets.PixelCrew.Core.Utils
{
    public static class GameObjectExtensions
    {
        public static bool IsInLayer(this GameObject gameObject, LayerMask layer)
        {
            return layer == (layer | 1 << gameObject.layer);
        }
    }
}
