// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using Object = UnityEngine.Object;

namespace Generated
{
    public class PlayerInput : IInputActionCollection, IDisposable
    {

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private readonly InputAction m_Gameplay_PointerDown;
        private readonly InputAction m_Gameplay_PointerPosition;

        // Menu
        private readonly InputActionMap m_Menu;
        private readonly InputAction m_Menu_Newaction;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private IMenuActions m_MenuActionsCallbackInterface;
        public PlayerInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""e6652b49-55ae-4e39-8e3f-daf65cbe51fd"",
            ""actions"": [
                {
                    ""name"": ""PointerPosition"",
                    ""type"": ""Value"",
                    ""id"": ""91243fab-6e10-4a49-825a-03c0074da3b7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PointerDown"",
                    ""type"": ""Value"",
                    ""id"": ""8be1b6d9-47f9-40d9-90a1-b7170e1f8ab6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ecce1247-223f-4a22-b962-e901142cfa15"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PointerPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""544e504c-cfd6-41be-862e-b921e184cb51"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PointerDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""886aa11a-65f3-400c-8bfd-5955d206415f"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""fd0421ad-7a38-4d65-90eb-c03599015d18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7b93dc9a-c51b-40d1-97a6-36ffd45e8e85"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", true);
            m_Gameplay_PointerPosition = m_Gameplay.FindAction("PointerPosition", true);
            m_Gameplay_PointerDown = m_Gameplay.FindAction("PointerDown", true);
            // Menu
            m_Menu = asset.FindActionMap("Menu", true);
            m_Menu_Newaction = m_Menu.FindAction("New action", true);
        }

        public InputActionAsset asset { get; }

        public GameplayActions Gameplay => new GameplayActions(this);

        public MenuActions Menu => new MenuActions(this);

        public void Dispose() => Object.Destroy(asset);

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action) => asset.Contains(action);

        public IEnumerator<InputAction> GetEnumerator() => asset.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Enable() => asset.Enable();

        public void Disable() => asset.Disable();

        public struct GameplayActions
        {
            private readonly PlayerInput m_Wrapper;
            public GameplayActions(PlayerInput wrapper) => m_Wrapper = wrapper;

            public InputAction PointerPosition => m_Wrapper.m_Gameplay_PointerPosition;

            public InputAction PointerDown => m_Wrapper.m_Gameplay_PointerDown;

            public InputActionMap Get() => m_Wrapper.m_Gameplay;
            public void Enable() => Get().Enable();
            public void Disable() => Get().Disable();

            public bool enabled => Get().enabled;

            public static implicit operator InputActionMap(GameplayActions set) => set.Get();
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    PointerPosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerPosition;
                    PointerPosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerPosition;
                    PointerPosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerPosition;
                    PointerDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerDown;
                    PointerDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerDown;
                    PointerDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPointerDown;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    PointerPosition.started += instance.OnPointerPosition;
                    PointerPosition.performed += instance.OnPointerPosition;
                    PointerPosition.canceled += instance.OnPointerPosition;
                    PointerDown.started += instance.OnPointerDown;
                    PointerDown.performed += instance.OnPointerDown;
                    PointerDown.canceled += instance.OnPointerDown;
                }
            }
        }

        public struct MenuActions
        {
            private readonly PlayerInput m_Wrapper;
            public MenuActions(PlayerInput wrapper) => m_Wrapper = wrapper;

            public InputAction Newaction => m_Wrapper.m_Menu_Newaction;

            public InputActionMap Get() => m_Wrapper.m_Menu;
            public void Enable() => Get().Enable();
            public void Disable() => Get().Disable();

            public bool enabled => Get().enabled;

            public static implicit operator InputActionMap(MenuActions set) => set.Get();
            public void SetCallbacks(IMenuActions instance)
            {
                if (m_Wrapper.m_MenuActionsCallbackInterface != null)
                {
                    Newaction.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnNewaction;
                    Newaction.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnNewaction;
                    Newaction.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnNewaction;
                }
                m_Wrapper.m_MenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Newaction.started += instance.OnNewaction;
                    Newaction.performed += instance.OnNewaction;
                    Newaction.canceled += instance.OnNewaction;
                }
            }
        }

        public interface IGameplayActions
        {
            void OnPointerPosition(InputAction.CallbackContext context);
            void OnPointerDown(InputAction.CallbackContext context);
        }

        public interface IMenuActions
        {
            void OnNewaction(InputAction.CallbackContext context);
        }
    }
}