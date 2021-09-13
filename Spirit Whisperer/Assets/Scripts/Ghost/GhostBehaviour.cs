using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostPatience
{    
    NONE, //Extremely likely to respond(90% chance to respond)
    CALM, //More likely to respond, less likely to attack, extremely unlikely to get angrier on a sucessfull response(65% chance to respond, 15% to get angry on a successful response)
    IMPATIENT, //Somewhat likely to respond,more likely to attack, unlikely to get angrier on a sucessfull response(50% chance to respond, 25% to get angry on a sucessful response)
    ANGRY //Extremely unlikely to respond,extremely likely to attack , already at the max patience level and it cannot get angrier(30% chance to respond)
}

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] float failureChance;
    [SerializeField] float AngerOnFailedResponse;
    [SerializeField] float AngerOnSucessfullResponse;
    [SerializeField] GhostPatience currentPatience;

    bool isPresent = false;
    bool fullNameRevealed = false;
    bool ageRevealed = false;
    bool dateOfBirthRevealed = false;
    bool dateOfDeathRevealed = false;
    bool genderRevealed = false;
    bool causeOfDeathRevealed = false;


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
        currentPatience = GhostPatience.NONE;
        SetUpPatience();
        SetUpInformationPanelUI();
    }

    void SetUpPatience()
    {
        switch (currentPatience)
        {
            case GhostPatience.NONE:
                failureChance = 0.10f;
                AngerOnFailedResponse = 0f;
                AngerOnSucessfullResponse = 0f;
                break;
            case GhostPatience.CALM:
                failureChance = 0.35f;
                AngerOnSucessfullResponse = 0.10f;
                AngerOnFailedResponse = 0.50f;
                break;
            case GhostPatience.IMPATIENT:
                failureChance = 0.5f;
                AngerOnSucessfullResponse = 0.15f;
                AngerOnFailedResponse = 0.50f;
                break;
            case GhostPatience.ANGRY:
                failureChance = 0.75f;
                AngerOnSucessfullResponse = 0f;
                AngerOnFailedResponse = 0f;
                break;
        }
    }


    void Respond(int whichReveal)
    {
        
        if (isResponseSuccessfull())
        {
            RevealInformation(whichReveal);
            GameActions.onResponseSucceded?.Invoke();
            GetAngrier(AngerOnSucessfullResponse);
        }
        else 
        {
            GetAngrier(AngerOnFailedResponse);
            GameActions.onResponseFailed?.Invoke();
        }
    }


    void GetAngrier(float chance)
    {
        if (CanGetAngry(chance))
        {
            Debug.Log("The ghost is angrier");
            switch (currentPatience)
            {
                case GhostPatience.NONE:
                    currentPatience = GhostPatience.CALM;
                    break;
                case GhostPatience.CALM:
                    currentPatience = GhostPatience.IMPATIENT;
                    break;
                case GhostPatience.IMPATIENT:
                    currentPatience = GhostPatience.ANGRY;
                    break;
            }
            SetUpPatience();
        }
        else
        {
            return;
        }
    }


    private bool CanGetAngry(float failureChance)
    {
        if(currentPatience == GhostPatience.NONE)
        {
            return true;
        }
        float currentChance = Random.Range(0f, 1f);
        //Debug.Log(currentChance);
        if(currentChance <= failureChance && currentPatience != GhostPatience.ANGRY && isPresent)
        {
            return true;
        }
        return false;
    }

    private bool isResponseSuccessfull()
    {
        float currentChance = Random.Range(0f, 1f);
        //Debug.Log(currentChange);
        if(currentChance >= failureChance)
        {
            return true;
        }
        return false;
    }

    void RevealInformation(int which)
    {
        switch (which)
        {
            case 0:
                isPresent = true;
                break;
            case 1:
                fullNameRevealed = true;
                break;
            case 2:
                ageRevealed = true;
                break;
            case 3:
                dateOfBirthRevealed = true;
                break;
            case 4:
                dateOfDeathRevealed = true;
                break;
            case 5:
                genderRevealed = true;
                break;
            case 6:
                causeOfDeathRevealed = true;
                break;
        }
        SetUpInformationPanelUI();

    }


    void SetUpInformationPanelUI()
    {
        if (!fullNameRevealed)
        {
            InformationPanelUI.instance.SetUpFullName("???");
        }
        else
        {
            InformationPanelUI.instance.SetUpFullName(GhostData.Instance.FullName);
        }

        if(!ageRevealed)
        {
            InformationPanelUI.instance.SetUpAge("???");
        }
        else
        {
            InformationPanelUI.instance.SetUpAge(GhostData.Instance.Age);
        }

        if (!dateOfBirthRevealed)
        {
            InformationPanelUI.instance.SetUpBirth("???");
        }
        else
        {
            InformationPanelUI.instance.SetUpBirth(GhostData.Instance.DateOfBirth);
        }

        if (!dateOfDeathRevealed)
        {
            InformationPanelUI.instance.SetUpDeath("???");
        }
        else
        {
            InformationPanelUI.instance.SetUpDeath(GhostData.Instance.DateOfDeath);
        }

        if (!genderRevealed)
        {
            InformationPanelUI.instance.SetUpGender("???");
        }
        else
        {
            InformationPanelUI.instance.SetUpGender(GhostData.Instance.Gender);
        }

        if (!causeOfDeathRevealed)
        {
            InformationPanelUI.instance.SetUpCauseOfDeath("???");
        }
        else
        {
            InformationPanelUI.instance.SetUpCauseOfDeath(GhostData.Instance.CauseOfDeath);
        }

        InformationPanelUI.instance.SetUpPresenceToggle(isPresent);
    }
}
