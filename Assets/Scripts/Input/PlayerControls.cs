// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""c01648f2-e437-4084-a099-cc0f8e0f1017"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""16570c73-4473-4abb-878e-48c89748ebc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""4be1464b-bc1c-4a72-a8f4-ab44528da6f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AlternateAttack"",
                    ""type"": ""Button"",
                    ""id"": ""423feea6-9231-4dbd-b1cc-c40e71490c27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectWeaponOne"",
                    ""type"": ""Button"",
                    ""id"": ""e128cbb2-1272-43bd-8d9d-55d16b6f48bc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectWeaponTwo"",
                    ""type"": ""Button"",
                    ""id"": ""4424b4e7-30de-453c-8e34-b82ea2a61411"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectWeaponThree"",
                    ""type"": ""Button"",
                    ""id"": ""2d32ce9d-84cb-4c74-b2a5-05935f9e6372"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""1a3a65fb-e219-4d9a-ad9a-3022ffe07b0e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""85a5bc47-a395-4bba-a959-31449be64894"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a42137d0-7707-41aa-bb3b-dc366e0e2d34"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""af667743-107e-4343-a81c-df5a73b22b56"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""785adc75-8bb5-46e7-ab1a-ba4f490338a7"",
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
                    ""id"": ""2dd6a721-038f-4276-8760-5d2d7b122c77"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a358721b-dd19-4f1b-9f0b-16ddf9dbd775"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AlternateAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""77a5140c-ad51-491f-a5cc-3c86d10b32f8"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeaponOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7b4a974-e390-4ac3-bb2a-441c8ce5b06b"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeaponTwo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""685922a0-60a0-4ffc-b151-76af58abf2ba"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeaponThree"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_AlternateAttack = m_Player.FindAction("AlternateAttack", throwIfNotFound: true);
        m_Player_SelectWeaponOne = m_Player.FindAction("SelectWeaponOne", throwIfNotFound: true);
        m_Player_SelectWeaponTwo = m_Player.FindAction("SelectWeaponTwo", throwIfNotFound: true);
        m_Player_SelectWeaponThree = m_Player.FindAction("SelectWeaponThree", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_AlternateAttack;
    private readonly InputAction m_Player_SelectWeaponOne;
    private readonly InputAction m_Player_SelectWeaponTwo;
    private readonly InputAction m_Player_SelectWeaponThree;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @AlternateAttack => m_Wrapper.m_Player_AlternateAttack;
        public InputAction @SelectWeaponOne => m_Wrapper.m_Player_SelectWeaponOne;
        public InputAction @SelectWeaponTwo => m_Wrapper.m_Player_SelectWeaponTwo;
        public InputAction @SelectWeaponThree => m_Wrapper.m_Player_SelectWeaponThree;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @AlternateAttack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAlternateAttack;
                @AlternateAttack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAlternateAttack;
                @AlternateAttack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAlternateAttack;
                @SelectWeaponOne.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponOne;
                @SelectWeaponOne.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponOne;
                @SelectWeaponOne.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponOne;
                @SelectWeaponTwo.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponTwo;
                @SelectWeaponTwo.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponTwo;
                @SelectWeaponTwo.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponTwo;
                @SelectWeaponThree.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponThree;
                @SelectWeaponThree.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponThree;
                @SelectWeaponThree.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectWeaponThree;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @AlternateAttack.started += instance.OnAlternateAttack;
                @AlternateAttack.performed += instance.OnAlternateAttack;
                @AlternateAttack.canceled += instance.OnAlternateAttack;
                @SelectWeaponOne.started += instance.OnSelectWeaponOne;
                @SelectWeaponOne.performed += instance.OnSelectWeaponOne;
                @SelectWeaponOne.canceled += instance.OnSelectWeaponOne;
                @SelectWeaponTwo.started += instance.OnSelectWeaponTwo;
                @SelectWeaponTwo.performed += instance.OnSelectWeaponTwo;
                @SelectWeaponTwo.canceled += instance.OnSelectWeaponTwo;
                @SelectWeaponThree.started += instance.OnSelectWeaponThree;
                @SelectWeaponThree.performed += instance.OnSelectWeaponThree;
                @SelectWeaponThree.canceled += instance.OnSelectWeaponThree;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnAlternateAttack(InputAction.CallbackContext context);
        void OnSelectWeaponOne(InputAction.CallbackContext context);
        void OnSelectWeaponTwo(InputAction.CallbackContext context);
        void OnSelectWeaponThree(InputAction.CallbackContext context);
    }
}
