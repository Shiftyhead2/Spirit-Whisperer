using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostPatience
{
    CALM, //More likely to respond, less likely to attack, extremely unlikely to get angrier on a sucessfull response(75% chance to respond)
    IMPATIENT, //Somewhat likely to respond,more likely to attack, unlikely to get angrier on a sucessfull response(50% chance to respond)
    ANGRY //Extremely unlikely to respond,extremely likely to attack , already at the max patience level and it cannot get angrier(25% chance to respond)
}

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] float failureChance; 
    [SerializeField] GhostPatience currentPatience;


    void OnEnable()
    {
        GameActions.onAwaitResponse += Respond;
    }

    void OnDisable()
    {
        GameActions.onAwaitResponse -= Respond;
    }


    void Start()
    {
        SetUpPatience();
    }

    void SetUpPatience()
    {
        switch (currentPatience)
        {
            case GhostPatience.CALM:
                failureChance = 0.25f;
                break;
            case GhostPatience.IMPATIENT:
                failureChance = 0.5f;
                break;
            case GhostPatience.ANGRY:
                failureChance = 0.75f;
                break;
        }
    }


    void Respond()
    {
        if (isResponseSuccessfull())
        {
            GameActions.onResponseSucceded?.Invoke();
        }
        else 
        {

            GameActions.onResponseFailed?.Invoke();
        }
    }

    private bool isResponseSuccessfull()
    {
        float currentChange = Random.Range(0f, 1f);
        Debug.Log(currentChange);
        if(currentChange >= failureChance)
        {
            return true;
        }
        return false;
    }
}
