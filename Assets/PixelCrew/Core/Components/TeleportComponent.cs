using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.PixelCrew.Core.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform _destTransform;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;

        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var sprite = target.GetComponent<SpriteRenderer>();
            var input = target.GetComponent<PlayerInput>();

            if (sprite == null)
            {
                yield break;
            }

            SetLockInput(input, true);
            var originalAlpha = sprite.color.a;

            yield return AlphaAnimation(sprite, originalAlpha, 0, _alphaTime);

            sprite.enabled = false;
            SetColliders(target, false);

            yield return MoveAnimation(target);

            sprite.enabled = true;
            SetColliders(target, true);
            yield return AlphaAnimation(sprite, 0, originalAlpha, _alphaTime);

            SetLockInput(input, false);
        }

        private void SetLockInput(PlayerInput input, bool isLocked)
        {
            if (input != null)
            {
                input.enabled = !isLocked;
            }
        }

        private void SetColliders(GameObject target, bool enabled)
        {
            var colliders = target.GetComponents<Collider2D>();
            foreach (var collider in colliders)
            {
                collider.enabled = enabled;
            }
        }

        private IEnumerator MoveAnimation(GameObject target)
        {
            var moveTime = 0f;
            var startPos = target.transform.position;
            var endPos = _destTransform.position;

            while (moveTime < _moveTime)
            {
                moveTime += Time.deltaTime;
                var progress = moveTime / _moveTime;
                target.transform.position = Vector3.Lerp(startPos, endPos, progress);
                yield return null;
            }

            target.transform.position = endPos;
        }

        private IEnumerator AlphaAnimation(SpriteRenderer sprite, float startAlpha, float destAlpha, float time)
        {
            var elapsed = 0f;

            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                var progress = elapsed / time;
                var currentAlpha = Mathf.Lerp(startAlpha, destAlpha, progress);

                var color = sprite.color;
                color.a = currentAlpha;
                sprite.color = color;

                yield return null;
            }

            var finalColor = sprite.color;
            finalColor.a = destAlpha;
            sprite.color = finalColor;
        }
    }
}