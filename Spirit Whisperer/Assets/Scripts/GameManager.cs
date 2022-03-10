using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] float huntPercentIncrease = 0.1f;
    [SerializeField] float HuntDuration = 60f;
    [SerializeField] float timeTick = 2f;
    float timeToStartTicking = 1f;
    float currentHuntPercent = 0f;
    float currentHuntDuration = 0f;

    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Camera jumpScareCamera;
    [SerializeField]
    GameObject jumpScarePlane;

    AudioListener playerAudioListener;
    AudioListener jumpScareAudioListener;



    public static bool isHuntActivated = false;
    public static bool isJumpscared = false;


    private void OnEnable()
    {
        GameActions.onJumpScare += OnJumpScare;
    }


    private void OnDisable()
    {
        GameActions.onJumpScare -= OnJumpScare;
    }


    private void Awake()
    {

        playerCamera = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Camera>();
        jumpScareCamera = GameObject.FindGameObjectWithTag("AI").GetComponentInChildren<Camera>();
        jumpScarePlane = GameObject.FindGameObjectWithTag("Jumpscare");


        if(playerCamera != null)
        {
            playerCamera.enabled = true;
            playerAudioListener = playerCamera.transform.GetComponent<AudioListener>();
            playerAudioListener.enabled = true;
        }
        else
        {
            Debug.LogError($"Unable to find a player camera. It's either not tagged properly or it doesn't exist!");
        }

        if(jumpScareCamera != null)
        {
            jumpScareCamera.enabled = false;
            jumpScareAudioListener = jumpScareCamera.transform.GetComponent<AudioListener>();
            jumpScareAudioListener.enabled = false;
        }
        else
        {
            Debug.LogError($"Unable to find jumpscare camera. It's either not tagged properly or it doesn't exist!");
        }

        jumpScarePlane.SetActive(false);



        currentHuntPercent = 0f;
        isHuntActivated = false;
        isJumpscared = false;
        currentHuntDuration = HuntDuration;
        StartHuntTickDown();
    }


    void StartHuntTickDown()
    {
        InvokeRepeating(nameof(IncreaseHuntPercentange), timeToStartTicking, timeTick);
    }



    private void Update()
    {

        if (!isJumpscared)
        {
            if (isHuntActivated)
            {
                currentHuntDuration -= Time.deltaTime;
                if (currentHuntDuration <= 0f)
                {
                    Debug.Log("Ending the hunt");
                    EndHunt();
                }
            }
        }
        else
        {
            return;
        }
    }


    void IncreaseHuntPercentange()
    {
        currentHuntPercent += huntPercentIncrease;

        if(currentHuntPercent >= 1f)
        {
            Debug.Log("Starting a hunt");
            StartHunt();
        }
    }


    void StartHunt()
    {
        GameActions.onHuntStart?.Invoke();
        isHuntActivated = true;
        currentHuntPercent = 0f;
        currentHuntDuration = HuntDuration;
        CancelInvoke();
    }


    void EndHunt()
    {
        GameActions.onHuntEnd?.Invoke();
        isHuntActivated = false;
        currentHuntPercent = 0f;
        StartHuntTickDown();
    }


    void OnJumpScare()
    {
        isJumpscared = true;
        playerAudioListener.enabled = false;
        playerCamera.enabled = false;
        jumpScareAudioListener.enabled = true;
        jumpScareCamera.enabled = true;
        jumpScarePlane.SetActive(true);
        CancelInvoke();
    }

}
