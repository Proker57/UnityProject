// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Engine/UIInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @UIInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIInput"",
    ""maps"": [
        {
            ""name"": ""UIActions"",
            ""id"": ""4572e144-7bcc-4930-b8bd-30ba47387b3f"",
            ""actions"": [
                {
                    ""name"": ""Point"",
                    ""type"": ""Button"",
                    ""id"": ""c6e9f016-fc60-4c1a-ae6b-a8d3e446f05d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""08718666-5cb0-4478-b081-b45ebe182348"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UIActions
        m_UIActions = asset.FindActionMap("UIActions", throwIfNotFound: true);
        m_UIActions_Point = m_UIActions.FindAction("Point", throwIfNotFound: true);
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

    // UIActions
    private readonly InputActionMap m_UIActions;
    private IUIActionsActions m_UIActionsActionsCallbackInterface;
    private readonly InputAction m_UIActions_Point;
    public struct UIActionsActions
    {
        private @UIInput m_Wrapper;
        public UIActionsActions(@UIInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Point => m_Wrapper.m_UIActions_Point;
        public InputActionMap Get() { return m_Wrapper.m_UIActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActionsActions set) { return set.Get(); }
        public void SetCallbacks(IUIActionsActions instance)
        {
            if (m_Wrapper.m_UIActionsActionsCallbackInterface != null)
            {
                @Point.started -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsActionsCallbackInterface.OnPoint;
            }
            m_Wrapper.m_UIActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
            }
        }
    }
    public UIActionsActions @UIActions => new UIActionsActions(this);
    public interface IUIActionsActions
    {
        void OnPoint(InputAction.CallbackContext context);
    }
}
