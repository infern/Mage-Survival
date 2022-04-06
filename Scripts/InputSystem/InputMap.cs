// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputSystem/InputMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""I"",
            ""id"": ""ee9d9142-ea0e-4633-b5b7-844ca637d5e6"",
            ""actions"": [
                {
                    ""name"": ""run"",
                    ""type"": ""Value"",
                    ""id"": ""69b6517b-57ae-45c2-96da-81403371442a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""attack"",
                    ""type"": ""Button"",
                    ""id"": ""aab17802-8799-4c1b-b908-aa32deaf1656"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""pause"",
                    ""type"": ""Button"",
                    ""id"": ""9d9f47c7-dce1-475a-980d-3e30f9d6221a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6f0998cd-c38a-4b6e-a607-dd92e57ba0a7"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a6b2e40-3dd7-45e3-9ef4-a0eddd7b758d"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b52c9af5-ba7f-4972-a9f4-7213f96ac2ab"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9eecd77d-bb99-4b03-a8db-1e376518ab14"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a0d2c95b-aceb-4368-84a2-fa63b0fb82e7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""run"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2260a592-568e-4387-aef4-9ac1c5de87fc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""33916e5c-b405-4c3e-a000-206e09f16ee7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""64f14328-351e-4e62-a9e7-dea667113a45"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""23efbb4b-8c2f-4032-ad95-b3e06720e2b4"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""aff0075f-e5da-4204-be76-eab477604b09"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // I
        m_I = asset.FindActionMap("I", throwIfNotFound: true);
        m_I_run = m_I.FindAction("run", throwIfNotFound: true);
        m_I_attack = m_I.FindAction("attack", throwIfNotFound: true);
        m_I_pause = m_I.FindAction("pause", throwIfNotFound: true);
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

    // I
    private readonly InputActionMap m_I;
    private IIActions m_IActionsCallbackInterface;
    private readonly InputAction m_I_run;
    private readonly InputAction m_I_attack;
    private readonly InputAction m_I_pause;
    public struct IActions
    {
        private @InputMap m_Wrapper;
        public IActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @run => m_Wrapper.m_I_run;
        public InputAction @attack => m_Wrapper.m_I_attack;
        public InputAction @pause => m_Wrapper.m_I_pause;
        public InputActionMap Get() { return m_Wrapper.m_I; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(IActions set) { return set.Get(); }
        public void SetCallbacks(IIActions instance)
        {
            if (m_Wrapper.m_IActionsCallbackInterface != null)
            {
                @run.started -= m_Wrapper.m_IActionsCallbackInterface.OnRun;
                @run.performed -= m_Wrapper.m_IActionsCallbackInterface.OnRun;
                @run.canceled -= m_Wrapper.m_IActionsCallbackInterface.OnRun;
                @attack.started -= m_Wrapper.m_IActionsCallbackInterface.OnAttack;
                @attack.performed -= m_Wrapper.m_IActionsCallbackInterface.OnAttack;
                @attack.canceled -= m_Wrapper.m_IActionsCallbackInterface.OnAttack;
                @pause.started -= m_Wrapper.m_IActionsCallbackInterface.OnPause;
                @pause.performed -= m_Wrapper.m_IActionsCallbackInterface.OnPause;
                @pause.canceled -= m_Wrapper.m_IActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_IActionsCallbackInterface = instance;
            if (instance != null)
            {
                @run.started += instance.OnRun;
                @run.performed += instance.OnRun;
                @run.canceled += instance.OnRun;
                @attack.started += instance.OnAttack;
                @attack.performed += instance.OnAttack;
                @attack.canceled += instance.OnAttack;
                @pause.started += instance.OnPause;
                @pause.performed += instance.OnPause;
                @pause.canceled += instance.OnPause;
            }
        }
    }
    public IActions @I => new IActions(this);
    public interface IIActions
    {
        void OnRun(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
