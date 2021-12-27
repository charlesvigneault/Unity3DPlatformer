//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Inputs/CharacterInputsController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @CharacterInputsController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CharacterInputsController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CharacterInputsController"",
    ""maps"": [
        {
            ""name"": ""CharacterMovements"",
            ""id"": ""f1b8decd-6265-415e-9a05-d5fad5753c6e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""62d45198-de2a-4920-af5d-2be58c1051bc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b616fcfb-d309-418c-8aff-2b444ace4921"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""49ebd158-4ab0-4181-aaf0-025bf1a153eb"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""22e0f650-5944-4c5a-96ef-4c600c6c731a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cc384503-a53e-4700-992a-5e7862152f6c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e9432cb7-ad79-48bb-a1d1-255c05135eb5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e28d5796-7b8d-4387-9fc6-e0d998bdb55b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4f9cc6fc-bd78-443a-81ab-c49e9347baec"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""32672f5c-3d31-4df6-9bf7-c99ee8cc86b0"",
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
                    ""id"": ""aa1d4e7e-b7bb-4783-983e-e1604fed7445"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterMovements
        m_CharacterMovements = asset.FindActionMap("CharacterMovements", throwIfNotFound: true);
        m_CharacterMovements_Move = m_CharacterMovements.FindAction("Move", throwIfNotFound: true);
        m_CharacterMovements_Jump = m_CharacterMovements.FindAction("Jump", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CharacterMovements
    private readonly InputActionMap m_CharacterMovements;
    private ICharacterMovementsActions m_CharacterMovementsActionsCallbackInterface;
    private readonly InputAction m_CharacterMovements_Move;
    private readonly InputAction m_CharacterMovements_Jump;
    public struct CharacterMovementsActions
    {
        private @CharacterInputsController m_Wrapper;
        public CharacterMovementsActions(@CharacterInputsController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_CharacterMovements_Move;
        public InputAction @Jump => m_Wrapper.m_CharacterMovements_Jump;
        public InputActionMap Get() { return m_Wrapper.m_CharacterMovements; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterMovementsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterMovementsActions instance)
        {
            if (m_Wrapper.m_CharacterMovementsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CharacterMovementsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CharacterMovementsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CharacterMovementsActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_CharacterMovementsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterMovementsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterMovementsActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_CharacterMovementsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public CharacterMovementsActions @CharacterMovements => new CharacterMovementsActions(this);
    public interface ICharacterMovementsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
