using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FPS_models;

public class FPSController : MonoBehaviour
{

    private bool ShouldCrouch => canCrouch && !duringCrouchAnimation && characterController.isGrounded;

    private CharacterController characterController;
    private PlayerControls inputActions;
    [SerializeField] Vector2 input_Movement;
    [SerializeField] Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("Functional Options")]
    [SerializeField] private bool canCrouch = true;

    [Header("References")]
    [SerializeField] Transform cameraHolder;
    [SerializeField] Light flashLight;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    [Header("View Clamp Settings")]
    [SerializeField] float viewClampYMin = -70f;
    [SerializeField] float viewClampYMax = 80f;

    [Header("Gravity")]
    [SerializeField] float gravityAmount;
    [SerializeField] float gravityMin;
    [SerializeField] float playerGravity;

    [Header("Crouch Parameters")]
    [SerializeField] float crouchHeight = 0.5f;
    [SerializeField] float standingHeight = 2f;
    [SerializeField] float timeToCrouch = 0.25f;
    [SerializeField] Vector3 crouchingCenter = new Vector3(0,0.5f,0);
    [SerializeField] Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;


    private void Awake()
    {
        inputActions = new PlayerControls();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Player.Movement.performed += ctx => input_Movement = ctx.ReadValue<Vector2>();
        inputActions.Player.View.performed += ctx => input_View = ctx.ReadValue<Vector2>();
        inputActions.Player.ToggleInformation.performed += ctx => GameActions.onToggleInformation?.Invoke();
        inputActions.Player.ToggleQuestions.performed += ctx => GameActions.onTogglePanels?.Invoke();
        inputActions.Player.AskQuestion1.performed += ctx => GameActions.onQuestionAsked?.Invoke(0);
        inputActions.Player.AskQuestion2.performed += ctx => GameActions.onQuestionAsked?.Invoke(1);
        inputActions.Player.ToggleFlashlight.performed += ctx => ToggleFlashlight();
        inputActions.Player.Crouch.performed += ctx => HandleCrouch();


        inputActions.Enable();
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

        if (playerGravity > gravityMin)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }


        if (playerGravity < -0.1f && characterController.isGrounded)
        {
            playerGravity = -0.1f;
        }

        newMovementSpeed.y += playerGravity;

        characterController.Move(newMovementSpeed);
    }

    void ToggleFlashlight()
    {
        flashLight.enabled = !flashLight.enabled;
    }

    void HandleCrouch()
    {
        if(ShouldCrouch)
        {
            StartCoroutine(CrouchStand());
        }
    }

    private IEnumerator CrouchStand()
    {
        duringCrouchAnimation = true;
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while(timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }
}
