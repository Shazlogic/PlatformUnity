using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Gameplay.Hero.Components
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;

        private HeroInputAction _inputActions;

        private void Awake()
        {
            _inputActions = new HeroInputAction();

            _inputActions.Hero.Movement.performed += OnMovement;
            _inputActions.Hero.Movement.canceled += OnMovement;

            _inputActions.Hero.SaySomething.performed += OnSaySomething;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnSaySomething(InputAction.CallbackContext context)
        {
            _hero.SaySomething();
        }
    }
}