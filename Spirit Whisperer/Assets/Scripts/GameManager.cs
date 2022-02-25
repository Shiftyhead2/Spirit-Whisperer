using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] float huntPercentIncrease = 0.1f;
    [SerializeField] float HuntDuration = 60f;
    [SerializeField] float timeTick = 2f;
    float currentHuntPercent = 0f;
    float currentHuntDuration = 0f;


    public static bool isHuntActivated = false;


    private void Awake()
    {
        currentHuntPercent = 0f;
        isHuntActivated = false;
        currentHuntDuration = HuntDuration;
        StartHuntTickDown();
    }


    void StartHuntTickDown()
    {
        InvokeRepeating(nameof(IncreaseHuntPercentange), 0f, timeTick);
    }



    private void Update()
    {
        if (isHuntActivated)
        {
            currentHuntDuration -= Time.deltaTime;
            if(currentHuntDuration <= 0f)
            {
                Debug.Log("Ending the hunt");
                EndHunt();
            }
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
        StartHuntTickDown();
    }

}
