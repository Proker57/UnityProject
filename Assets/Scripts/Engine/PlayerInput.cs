// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Engine/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""PlayerInGame"",
            ""id"": ""e6a3a0b8-3699-4f0b-a8b9-2b57e084e967"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b2d808f7-8206-4ccd-8481-2953f062d301"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4956d367-40bd-44af-9bbb-e78e2cdf6e1a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""6900673f-4ee3-4079-bb76-b01c4c3ef3d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""4fea6292-4651-4017-ac5d-f2c4cbfdaa9d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""c818d85a-8704-4261-9993-a755f4385b38"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""e54464e4-348d-47d1-b481-d43421333724"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4a580955-c285-40ef-b784-8237e69247f0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d3b8a5f6-7e0d-4e51-aee0-7c87b0e364a6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8261f702-5538-435d-8fa6-186795bed007"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5075144f-10b3-493c-81f0-cbfa6be6429e"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Global"",
            ""id"": ""eef72f26-747c-494b-b426-d1dd3aaadbb3"",
            ""actions"": [
                {
                    ""name"": ""Save"",
                    ""type"": ""Button"",
                    ""id"": ""8b8b5aa7-f712-43fe-af50-9e6e20f379f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Load"",
                    ""type"": ""Button"",
                    ""id"": ""6bd1b8e1-b991-45f1-bdd6-6b694006bd9d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""21b80cb9-eb54-40e3-ab32-a5df982bbdd2"",
                    ""path"": ""<Keyboard>/f5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02a65c27-391d-4607-9400-9fb173e14925"",
                    ""path"": ""<Keyboard>/f9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Load"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInGame
        m_PlayerInGame = asset.FindActionMap("PlayerInGame", throwIfNotFound: true);
        m_PlayerInGame_Movement = m_PlayerInGame.FindAction("Movement", throwIfNotFound: true);
        m_PlayerInGame_Jump = m_PlayerInGame.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInGame_Crouch = m_PlayerInGame.FindAction("Crouch", throwIfNotFound: true);
        m_PlayerInGame_Dash = m_PlayerInGame.FindAction("Dash", throwIfNotFound: true);
        // Global
        m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
        m_Global_Save = m_Global.FindAction("Save", throwIfNotFound: true);
        m_Global_Load = m_Global.FindAction("Load", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

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

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerInGame
    private readonly InputActionMap m_PlayerInGame;
    private IPlayerInGameActions m_PlayerInGameActionsCallbackInterface;
    private readonly InputAction m_PlayerInGame_Movement;
    private readonly InputAction m_PlayerInGame_Jump;
    private readonly InputAction m_PlayerInGame_Crouch;
    private readonly InputAction m_PlayerInGame_Dash;
    public struct PlayerInGameActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerInGameActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerInGame_Movement;
        public InputAction @Jump => m_Wrapper.m_PlayerInGame_Jump;
        public InputAction @Crouch => m_Wrapper.m_PlayerInGame_Crouch;
        public InputAction @Dash => m_Wrapper.m_PlayerInGame_Dash;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInGameActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInGameActions instance)
        {
            if (m_Wrapper.m_PlayerInGameActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnCrouch;
                @Dash.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_PlayerInGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public PlayerInGameActions @PlayerInGame => new PlayerInGameActions(this);

    // Global
    private readonly InputActionMap m_Global;
    private IGlobalActions m_GlobalActionsCallbackInterface;
    private readonly InputAction m_Global_Save;
    private readonly InputAction m_Global_Load;
    public struct GlobalActions
    {
        private @PlayerInput m_Wrapper;
        public GlobalActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Save => m_Wrapper.m_Global_Save;
        public InputAction @Load => m_Wrapper.m_Global_Load;
        public InputActionMap Get() { return m_Wrapper.m_Global; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GlobalActions set) { return set.Get(); }
        public void SetCallbacks(IGlobalActions instance)
        {
            if (m_Wrapper.m_GlobalActionsCallbackInterface != null)
            {
                @Save.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnSave;
                @Save.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnSave;
                @Save.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnSave;
                @Load.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnLoad;
                @Load.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnLoad;
                @Load.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnLoad;
            }
            m_Wrapper.m_GlobalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Save.started += instance.OnSave;
                @Save.performed += instance.OnSave;
                @Save.canceled += instance.OnSave;
                @Load.started += instance.OnLoad;
                @Load.performed += instance.OnLoad;
                @Load.canceled += instance.OnLoad;
            }
        }
    }
    public GlobalActions @Global => new GlobalActions(this);
    public interface IPlayerInGameActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
    public interface IGlobalActions
    {
        void OnSave(InputAction.CallbackContext context);
        void OnLoad(InputAction.CallbackContext context);
    }
}
