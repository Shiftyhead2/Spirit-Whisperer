using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FPS_models;

public class FPSController : MonoBehaviour
{
    #region -LambaBools-
    private bool ShouldCrouch => canCrouch && !duringCrouchAnimation && characterController.isGrounded;
    private bool ShouldSprint => !isCrouching && !duringCrouchAnimation && characterController.isGrounded;
    #endregion


    #region -Variables-
    private CharacterController characterController;
    private PlayerControls inputActions;
    Vector2 input_Movement;
    Vector2 input_View;

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


    [Header("Leaning")]
    public Transform LeanPivot;
    private float currentLean;
    private float targetLean;
    public float leanAngle;
    public float leanSmoothing;
    private float leanVelocity;


    private bool isLeaningLeft = false;
    private bool isLeaningRight = false;

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
    private bool isSprinting;


    private Vector3 newMovementSpeed;
    private Vector3 newMovementVelocity;
    #endregion

    #region -Awake-
    private void Awake()
    {
        inputActions = new PlayerControls();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    #endregion

    #region -OnEnable/OnDisable-
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
        inputActions.Player.ToggleSprint.performed += ctx => ToggleSprint();
        inputActions.Player.SprintReleased.performed += ctx => StopSprint();

        inputActions.Player.LeanLeftPressed.performed += ctx => isLeaningLeft = true;
        inputActions.Player.LeanLeftPressed.canceled += ctx => isLeaningLeft = false;


        inputActions.Player.LeanRightPressed.performed += ctx => isLeaningRight = true;
        inputActions.Player.LeanRightPressed.canceled += ctx => isLeaningRight = false;

        GameActions.onJumpScare += onJumpScare;


        inputActions.Enable();
    }

    private void OnDisable()
    {
        GameActions.onJumpScare -= onJumpScare;

        inputActions.Disable();
    }
    #endregion

    #region -Update-
    private void Update()
    {
        CalculateMovement();
        CalculateView();
        CalculateLeaning();
    }
    #endregion

    #region -JumpScareStuff-
    void onJumpScare()
    {
        inputActions.Disable();
    }
    #endregion

    #region -ViewCalculation/MovementCalculation-
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

        if(input_Movement.y <= 0.2f || !ShouldSprint)
        {
            isSprinting = false;
        }



        var verticalSpeed = playerSettings.WalkingForwardSpeed;
        var horizontalSpeed = playerSettings.WalkingStrafeSpeed;


        if (isSprinting)
        {
            verticalSpeed = playerSettings.RunningForwardSpeed;
            horizontalSpeed = playerSettings.RunningStrafeSpeed;
        }


        if (!characterController.isGrounded)
        {
            playerSettings.SpeedEffector = playerSettings.FallingSpeedEffector;
        }
        else if (isCrouching)
        {
            playerSettings.SpeedEffector = playerSettings.CrouchSpeedEffector;
        }
        else
        {
            playerSettings.SpeedEffector = 1f;
        }


        //Effectors
        verticalSpeed *= playerSettings.SpeedEffector;
        horizontalSpeed *= playerSettings.SpeedEffector;



        newMovementSpeed = Vector3.SmoothDamp(newMovementSpeed, new Vector3(horizontalSpeed * input_Movement.x * Time.deltaTime, 0, verticalSpeed * input_Movement.y * Time.deltaTime),ref newMovementVelocity,playerSettings.MovementSmoothing);
        var movementSpeed = transform.TransformDirection(newMovementSpeed);

        if (playerGravity > gravityMin)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }


        if (playerGravity < -0.1f && characterController.isGrounded)
        {
            playerGravity = -0.1f;
        }

        movementSpeed.y += playerGravity;

        characterController.Move(movementSpeed);
    }
    #endregion

    #region -Leaning-

    private void CalculateLeaning()
    {

        if (isLeaningLeft)
        {
            targetLean = leanAngle;
        }
        else if (isLeaningRight)
        {
            targetLean = -leanAngle;
        }
        else
        {
            targetLean = 0;
        }



        currentLean = Mathf.SmoothDamp(currentLean, targetLean, ref leanVelocity, leanSmoothing);


        LeanPivot.localRotation = Quaternion.Euler(new Vector3(0, 0, currentLean));
    }

    #endregion

    #region -Flashlight-
    void ToggleFlashlight()
    {
        flashLight.enabled = !flashLight.enabled;
    }
    #endregion

    #region -Crouching-
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
    #endregion

    #region -Sprinting-
    private void ToggleSprint()
    {

        if (!ShouldSprint)
        {
            isSprinting = false;
            return;
        }




        if (input_Movement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }



        isSprinting = !isSprinting;
    }


    private void StopSprint()
    {

        if (playerSettings.sprintHold)
        {
            isSprinting = false;
        }
    }
    #endregion
}
