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
            ""id"": ""46b75866-d830-41d6-bf82-522d8ec78b34"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""61d87062-9b3a-4f8e-9cb9-10194f3f30ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""View"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7098e9db-aa79-45e6-a29c-68d2ce37641b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ToggleQuestions"",
                    ""type"": ""Button"",
                    ""id"": ""23a647b1-a919-4536-9799-5696d9ec6e2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ToggleInformation"",
                    ""type"": ""Button"",
                    ""id"": ""416e565c-2221-444a-b614-4a7a1887a833"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""AskQuestion1"",
                    ""type"": ""Button"",
                    ""id"": ""7464515f-e3a7-486d-982d-cad238833d1e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""AskQuestion2"",
                    ""type"": ""Button"",
                    ""id"": ""790a1ace-a40b-4be1-bbe2-3ae4df4e48aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ToggleFlashlight"",
                    ""type"": ""Button"",
                    ""id"": ""c78ae3f6-e7cb-4229-977d-0545faee9c1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""078268f2-e834-473f-bf4e-2df32ee66774"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ToggleSprint"",
                    ""type"": ""Button"",
                    ""id"": ""75c7d9d4-fa8e-4bc0-bb0f-9bfbf2ac7a69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""SprintReleased"",
                    ""type"": ""Button"",
                    ""id"": ""c6b67f47-50f6-4a8a-bad3-32242aaa85c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""LeanLeftPressed"",
                    ""type"": ""Button"",
                    ""id"": ""57a712c5-bb92-4a42-a38a-74bc88f07b2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                },
                {
                    ""name"": ""LeanRightPressed"",
                    ""type"": ""Button"",
                    ""id"": ""f9c37f32-8985-4386-a74b-07c936792f42"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ccdef71a-a400-420a-945f-e9fb697daa68"",
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
                    ""id"": ""51d0e2b6-7e81-48fa-a845-76cf9abd8690"",
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
                    ""id"": ""b237b215-f6ef-4fd9-a639-f1c99f25f675"",
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
                    ""id"": ""1a8312f1-48a7-460f-ac58-c81833fc4e43"",
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
                    ""id"": ""90aa5dce-1f07-4fbe-8122-f1bc28f4b60d"",
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
                    ""id"": ""748b0048-abaf-4697-a26e-2fcb46fa69af"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""View"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5667278e-d893-4fe5-9e62-6c2822275477"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleQuestions"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6141e716-db1c-40e4-8a40-d8e571d26c6f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleInformation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c49aeeee-938e-4459-af7b-4457030476b4"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AskQuestion2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9afe3c89-4a7c-4cd4-9904-d154e8a6280c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AskQuestion1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a775efb4-af37-4ef5-a037-20e4877eca5d"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleFlashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40ea869c-59f0-4e39-b710-5eebe9c81c6c"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fde7429a-8075-4321-abaa-f56a748d7218"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleSprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""714035a3-54a0-46ac-9413-ed9841ebaaaa"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintReleased"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97cbe3e1-1dd0-4b8e-8e41-f657e17edd16"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeanLeftPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c81c028-680d-4dd1-9814-6d23d7e12396"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeanRightPressed"",
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
        m_Player_View = m_Player.FindAction("View", throwIfNotFound: true);
        m_Player_ToggleQuestions = m_Player.FindAction("ToggleQuestions", throwIfNotFound: true);
        m_Player_ToggleInformation = m_Player.FindAction("ToggleInformation", throwIfNotFound: true);
        m_Player_AskQuestion1 = m_Player.FindAction("AskQuestion1", throwIfNotFound: true);
        m_Player_AskQuestion2 = m_Player.FindAction("AskQuestion2", throwIfNotFound: true);
        m_Player_ToggleFlashlight = m_Player.FindAction("ToggleFlashlight", throwIfNotFound: true);
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_ToggleSprint = m_Player.FindAction("ToggleSprint", throwIfNotFound: true);
        m_Player_SprintReleased = m_Player.FindAction("SprintReleased", throwIfNotFound: true);
        m_Player_LeanLeftPressed = m_Player.FindAction("LeanLeftPressed", throwIfNotFound: true);
        m_Player_LeanRightPressed = m_Player.FindAction("LeanRightPressed", throwIfNotFound: true);
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
    private readonly InputAction m_Player_View;
    private readonly InputAction m_Player_ToggleQuestions;
    private readonly InputAction m_Player_ToggleInformation;
    private readonly InputAction m_Player_AskQuestion1;
    private readonly InputAction m_Player_AskQuestion2;
    private readonly InputAction m_Player_ToggleFlashlight;
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_ToggleSprint;
    private readonly InputAction m_Player_SprintReleased;
    private readonly InputAction m_Player_LeanLeftPressed;
    private readonly InputAction m_Player_LeanRightPressed;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @View => m_Wrapper.m_Player_View;
        public InputAction @ToggleQuestions => m_Wrapper.m_Player_ToggleQuestions;
        public InputAction @ToggleInformation => m_Wrapper.m_Player_ToggleInformation;
        public InputAction @AskQuestion1 => m_Wrapper.m_Player_AskQuestion1;
        public InputAction @AskQuestion2 => m_Wrapper.m_Player_AskQuestion2;
        public InputAction @ToggleFlashlight => m_Wrapper.m_Player_ToggleFlashlight;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @ToggleSprint => m_Wrapper.m_Player_ToggleSprint;
        public InputAction @SprintReleased => m_Wrapper.m_Player_SprintReleased;
        public InputAction @LeanLeftPressed => m_Wrapper.m_Player_LeanLeftPressed;
        public InputAction @LeanRightPressed => m_Wrapper.m_Player_LeanRightPressed;
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
                @View.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnView;
                @View.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnView;
                @View.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnView;
                @ToggleQuestions.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleQuestions;
                @ToggleQuestions.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleQuestions;
                @ToggleQuestions.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleQuestions;
                @ToggleInformation.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInformation;
                @ToggleInformation.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInformation;
                @ToggleInformation.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInformation;
                @AskQuestion1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAskQuestion1;
                @AskQuestion1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAskQuestion1;
                @AskQuestion1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAskQuestion1;
                @AskQuestion2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAskQuestion2;
                @AskQuestion2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAskQuestion2;
                @AskQuestion2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAskQuestion2;
                @ToggleFlashlight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @ToggleFlashlight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleFlashlight;
                @Crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @ToggleSprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleSprint;
                @ToggleSprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleSprint;
                @ToggleSprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleSprint;
                @SprintReleased.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprintReleased;
                @SprintReleased.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprintReleased;
                @SprintReleased.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprintReleased;
                @LeanLeftPressed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeanLeftPressed;
                @LeanLeftPressed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeanLeftPressed;
                @LeanLeftPressed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeanLeftPressed;
                @LeanRightPressed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeanRightPressed;
                @LeanRightPressed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeanRightPressed;
                @LeanRightPressed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeanRightPressed;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @View.started += instance.OnView;
                @View.performed += instance.OnView;
                @View.canceled += instance.OnView;
                @ToggleQuestions.started += instance.OnToggleQuestions;
                @ToggleQuestions.performed += instance.OnToggleQuestions;
                @ToggleQuestions.canceled += instance.OnToggleQuestions;
                @ToggleInformation.started += instance.OnToggleInformation;
                @ToggleInformation.performed += instance.OnToggleInformation;
                @ToggleInformation.canceled += instance.OnToggleInformation;
                @AskQuestion1.started += instance.OnAskQuestion1;
                @AskQuestion1.performed += instance.OnAskQuestion1;
                @AskQuestion1.canceled += instance.OnAskQuestion1;
                @AskQuestion2.started += instance.OnAskQuestion2;
                @AskQuestion2.performed += instance.OnAskQuestion2;
                @AskQuestion2.canceled += instance.OnAskQuestion2;
                @ToggleFlashlight.started += instance.OnToggleFlashlight;
                @ToggleFlashlight.performed += instance.OnToggleFlashlight;
                @ToggleFlashlight.canceled += instance.OnToggleFlashlight;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @ToggleSprint.started += instance.OnToggleSprint;
                @ToggleSprint.performed += instance.OnToggleSprint;
                @ToggleSprint.canceled += instance.OnToggleSprint;
                @SprintReleased.started += instance.OnSprintReleased;
                @SprintReleased.performed += instance.OnSprintReleased;
                @SprintReleased.canceled += instance.OnSprintReleased;
                @LeanLeftPressed.started += instance.OnLeanLeftPressed;
                @LeanLeftPressed.performed += instance.OnLeanLeftPressed;
                @LeanLeftPressed.canceled += instance.OnLeanLeftPressed;
                @LeanRightPressed.started += instance.OnLeanRightPressed;
                @LeanRightPressed.performed += instance.OnLeanRightPressed;
                @LeanRightPressed.canceled += instance.OnLeanRightPressed;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnView(InputAction.CallbackContext context);
        void OnToggleQuestions(InputAction.CallbackContext context);
        void OnToggleInformation(InputAction.CallbackContext context);
        void OnAskQuestion1(InputAction.CallbackContext context);
        void OnAskQuestion2(InputAction.CallbackContext context);
        void OnToggleFlashlight(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnToggleSprint(InputAction.CallbackContext context);
        void OnSprintReleased(InputAction.CallbackContext context);
        void OnLeanLeftPressed(InputAction.CallbackContext context);
        void OnLeanRightPressed(InputAction.CallbackContext context);
    }
}
