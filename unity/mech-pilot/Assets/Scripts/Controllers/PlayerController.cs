using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Generated.PlayerInput;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput _controls;

        private void Awake() => _controls = new PlayerInput();
        private void Start()
        {
            _controls.Gameplay.PointerDown.started += OnTouchInteractionStarted;
            _controls.Gameplay.PointerDown.canceled += OnTouchInteractionStopped;
        }

        private void OnTouchInteractionStarted(InputAction.CallbackContext context) => throw new NotImplementedException();

        private void OnTouchInteractionStopped(InputAction.CallbackContext context) => throw new NotImplementedException();
    }
}