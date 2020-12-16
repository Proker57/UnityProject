// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Engine/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace BOYAREngine
{
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
                },
                {
                    ""name"": ""PrimaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""f3f73d72-d559-4ca5-8309-bce48831e555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""SecondaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9cf11737-b54f-4045-af1f-e818f4bfeb46"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""MeleePick"",
                    ""type"": ""Button"",
                    ""id"": ""902c35d1-3f67-4abc-b87b-fb6b5f5dbc2d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""RangePick"",
                    ""type"": ""Button"",
                    ""id"": ""f5641a42-7f9d-425c-bf24-5d80aa2bb575"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""5a03c11a-d1b1-4549-86c0-6770bbc6a57e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""ItemUse"",
                    ""type"": ""Button"",
                    ""id"": ""bcdc1fdb-a80d-4c38-af0d-65223917ddbb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""NextItem"",
                    ""type"": ""Button"",
                    ""id"": ""7a9f5daa-2963-41a4-9f15-db2e3e6f19d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""PreviousItem"",
                    ""type"": ""Button"",
                    ""id"": ""c4404f44-60d3-4fac-8e82-73455b2a9e70"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""cb159b31-09c5-480d-8d9d-8844f571effc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4df365c1-70e6-4f27-8076-a05138ef6901"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MeleePick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c007fd8-cdc4-4ec2-9b77-ac5b6824f4d4"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RangePick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f28f4577-a4ac-4977-a970-12dd11fc4125"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67b3aaea-0190-46c9-ba6a-0dbe7498b8c6"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ItemUse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce5f7f32-4a3a-49e9-ba18-5de406c5e97a"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2380f6e9-e560-4b91-b792-6f123dc0e4bc"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d436ba7-00e5-480b-a00e-00cf8333adce"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryAttack"",
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
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""57e0b4df-87fd-44b8-bdf8-939366754a41"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""40a8c164-d5f1-4eb4-b028-09dfb1204989"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
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
            m_PlayerInGame_PrimaryAttack = m_PlayerInGame.FindAction("PrimaryAttack", throwIfNotFound: true);
            m_PlayerInGame_SecondaryAttack = m_PlayerInGame.FindAction("SecondaryAttack", throwIfNotFound: true);
            m_PlayerInGame_MeleePick = m_PlayerInGame.FindAction("MeleePick", throwIfNotFound: true);
            m_PlayerInGame_RangePick = m_PlayerInGame.FindAction("RangePick", throwIfNotFound: true);
            m_PlayerInGame_Use = m_PlayerInGame.FindAction("Use", throwIfNotFound: true);
            m_PlayerInGame_ItemUse = m_PlayerInGame.FindAction("ItemUse", throwIfNotFound: true);
            m_PlayerInGame_NextItem = m_PlayerInGame.FindAction("NextItem", throwIfNotFound: true);
            m_PlayerInGame_PreviousItem = m_PlayerInGame.FindAction("PreviousItem", throwIfNotFound: true);
            // Global
            m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
            m_Global_Save = m_Global.FindAction("Save", throwIfNotFound: true);
            m_Global_Load = m_Global.FindAction("Load", throwIfNotFound: true);
            m_Global_Escape = m_Global.FindAction("Escape", throwIfNotFound: true);
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
        private readonly InputAction m_PlayerInGame_PrimaryAttack;
        private readonly InputAction m_PlayerInGame_SecondaryAttack;
        private readonly InputAction m_PlayerInGame_MeleePick;
        private readonly InputAction m_PlayerInGame_RangePick;
        private readonly InputAction m_PlayerInGame_Use;
        private readonly InputAction m_PlayerInGame_ItemUse;
        private readonly InputAction m_PlayerInGame_NextItem;
        private readonly InputAction m_PlayerInGame_PreviousItem;
        public struct PlayerInGameActions
        {
            private @PlayerInput m_Wrapper;
            public PlayerInGameActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_PlayerInGame_Movement;
            public InputAction @Jump => m_Wrapper.m_PlayerInGame_Jump;
            public InputAction @Crouch => m_Wrapper.m_PlayerInGame_Crouch;
            public InputAction @Dash => m_Wrapper.m_PlayerInGame_Dash;
            public InputAction @PrimaryAttack => m_Wrapper.m_PlayerInGame_PrimaryAttack;
            public InputAction @SecondaryAttack => m_Wrapper.m_PlayerInGame_SecondaryAttack;
            public InputAction @MeleePick => m_Wrapper.m_PlayerInGame_MeleePick;
            public InputAction @RangePick => m_Wrapper.m_PlayerInGame_RangePick;
            public InputAction @Use => m_Wrapper.m_PlayerInGame_Use;
            public InputAction @ItemUse => m_Wrapper.m_PlayerInGame_ItemUse;
            public InputAction @NextItem => m_Wrapper.m_PlayerInGame_NextItem;
            public InputAction @PreviousItem => m_Wrapper.m_PlayerInGame_PreviousItem;
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
                    @PrimaryAttack.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnPrimaryAttack;
                    @PrimaryAttack.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnPrimaryAttack;
                    @PrimaryAttack.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnPrimaryAttack;
                    @SecondaryAttack.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnSecondaryAttack;
                    @SecondaryAttack.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnSecondaryAttack;
                    @SecondaryAttack.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnSecondaryAttack;
                    @MeleePick.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnMeleePick;
                    @MeleePick.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnMeleePick;
                    @MeleePick.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnMeleePick;
                    @RangePick.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnRangePick;
                    @RangePick.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnRangePick;
                    @RangePick.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnRangePick;
                    @Use.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnUse;
                    @Use.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnUse;
                    @Use.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnUse;
                    @ItemUse.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnItemUse;
                    @ItemUse.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnItemUse;
                    @ItemUse.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnItemUse;
                    @NextItem.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnNextItem;
                    @NextItem.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnNextItem;
                    @NextItem.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnNextItem;
                    @PreviousItem.started -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnPreviousItem;
                    @PreviousItem.performed -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnPreviousItem;
                    @PreviousItem.canceled -= m_Wrapper.m_PlayerInGameActionsCallbackInterface.OnPreviousItem;
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
                    @PrimaryAttack.started += instance.OnPrimaryAttack;
                    @PrimaryAttack.performed += instance.OnPrimaryAttack;
                    @PrimaryAttack.canceled += instance.OnPrimaryAttack;
                    @SecondaryAttack.started += instance.OnSecondaryAttack;
                    @SecondaryAttack.performed += instance.OnSecondaryAttack;
                    @SecondaryAttack.canceled += instance.OnSecondaryAttack;
                    @MeleePick.started += instance.OnMeleePick;
                    @MeleePick.performed += instance.OnMeleePick;
                    @MeleePick.canceled += instance.OnMeleePick;
                    @RangePick.started += instance.OnRangePick;
                    @RangePick.performed += instance.OnRangePick;
                    @RangePick.canceled += instance.OnRangePick;
                    @Use.started += instance.OnUse;
                    @Use.performed += instance.OnUse;
                    @Use.canceled += instance.OnUse;
                    @ItemUse.started += instance.OnItemUse;
                    @ItemUse.performed += instance.OnItemUse;
                    @ItemUse.canceled += instance.OnItemUse;
                    @NextItem.started += instance.OnNextItem;
                    @NextItem.performed += instance.OnNextItem;
                    @NextItem.canceled += instance.OnNextItem;
                    @PreviousItem.started += instance.OnPreviousItem;
                    @PreviousItem.performed += instance.OnPreviousItem;
                    @PreviousItem.canceled += instance.OnPreviousItem;
                }
            }
        }
        public PlayerInGameActions @PlayerInGame => new PlayerInGameActions(this);

        // Global
        private readonly InputActionMap m_Global;
        private IGlobalActions m_GlobalActionsCallbackInterface;
        private readonly InputAction m_Global_Save;
        private readonly InputAction m_Global_Load;
        private readonly InputAction m_Global_Escape;
        public struct GlobalActions
        {
            private @PlayerInput m_Wrapper;
            public GlobalActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Save => m_Wrapper.m_Global_Save;
            public InputAction @Load => m_Wrapper.m_Global_Load;
            public InputAction @Escape => m_Wrapper.m_Global_Escape;
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
                    @Escape.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnEscape;
                    @Escape.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnEscape;
                    @Escape.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnEscape;
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
                    @Escape.started += instance.OnEscape;
                    @Escape.performed += instance.OnEscape;
                    @Escape.canceled += instance.OnEscape;
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
            void OnPrimaryAttack(InputAction.CallbackContext context);
            void OnSecondaryAttack(InputAction.CallbackContext context);
            void OnMeleePick(InputAction.CallbackContext context);
            void OnRangePick(InputAction.CallbackContext context);
            void OnUse(InputAction.CallbackContext context);
            void OnItemUse(InputAction.CallbackContext context);
            void OnNextItem(InputAction.CallbackContext context);
            void OnPreviousItem(InputAction.CallbackContext context);
        }
        public interface IGlobalActions
        {
            void OnSave(InputAction.CallbackContext context);
            void OnLoad(InputAction.CallbackContext context);
            void OnEscape(InputAction.CallbackContext context);
        }
    }
}
