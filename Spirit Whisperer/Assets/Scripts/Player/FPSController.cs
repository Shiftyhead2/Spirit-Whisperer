using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FPS_models;

public class FPSController : MonoBehaviour
{
    private CharacterController characterController;
    private PlayerControls inputActions;
    [SerializeField] Vector2 input_Movement;
    [SerializeField] Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    [SerializeField] Transform cameraHolder;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    [Header("View Clamp Settings")]
    [SerializeField] float viewClampYMin = -70f;
    [SerializeField] float viewClampYMax = 80f;

    [Header("Gravity")]
    [SerializeField] float gravityAmount;
    [SerializeField] float gravityMin;
    [SerializeField] float playerGravity;


    private void Awake()
    {
        inputActions = new PlayerControls();


        inputActions.Player.Movement.performed += ctx => input_Movement = ctx.ReadValue<Vector2>();
        inputActions.Player.View.performed += ctx => input_View = ctx.ReadValue<Vector2>();
        inputActions.Player.ToggleInformation.performed += _ => GameActions.onToggleInformation?.Invoke();
        inputActions.Player.ToggleQuestions.performed += _ => GameActions.onTogglePanels?.Invoke();
        inputActions.Player.AskQuestion1.performed += _ => GameActions.onQuestionAsked?.Invoke(0);
        inputActions.Player.AskQuestion2.performed += _ => GameActions.onQuestionAsked?.Invoke(1);

        inputActions.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        CalculateMovement();
        CalculateView();
    }


    void CalculateView()
    {
        newCharacterRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);


        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted ? input_View.y : -input_View.y) * Time.deltaTime;

        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax);


        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    void CalculateMovement()
    {
        var verticalSpeed = playerSettings.WalkingForwardSpeed * input_Movement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed * input_Movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        if(playerGravity > gravityMin)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }
        

        if(playerGravity < -0.1f && characterController.isGrounded)
        {
            playerGravity = -0.1f;
        }

        newMovementSpeed.y += playerGravity;

        characterController.Move(newMovementSpeed);
    }
}
