using Core.Input;
using Locomotion;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Generated.PlayerInput;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeReference] private Swipe swipe;
        [SerializeReference] private TouchInteraction startTouch;
        [SerializeReference] private TouchInteraction endTouch;
        private PlayerInput _controls;
        private ILocomotion _locomotion;

        private void Awake() => _controls = new PlayerInput();
        private void Start()
        {
            _controls.Gameplay.PointerDown.started += OnTouchInteractionStarted;
            _controls.Gameplay.PointerDown.canceled += OnTouchInteractionStopped;
            // TODO: complain is _locomotion is not set
            _locomotion = GetComponent<ILocomotion>();
        }
        private void OnEnable() => _controls?.Enable();
        private void OnDisable() => _controls?.Disable();

        private void OnTouchInteractionStarted(InputAction.CallbackContext context)
        {
            swipe = null;
            startTouch = TouchInteraction.Of(_controls.Gameplay.PointerPosition.ReadValue<Vector2>());
        }

        private void OnTouchInteractionStopped(InputAction.CallbackContext context)
        {
            endTouch = TouchInteraction.Of(_controls.Gameplay.PointerPosition.ReadValue<Vector2>());
            swipe = Swipe.Of(startTouch, endTouch);
            _locomotion.NewMovementDirection(swipe.VectorNormalized);
        }
    }
}