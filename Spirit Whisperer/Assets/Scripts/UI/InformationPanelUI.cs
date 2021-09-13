using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationPanelUI : MonoBehaviour
{
    public static InformationPanelUI instance { get; private set; }

    [SerializeField] Toggle PresenceToggler;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI AgeText;
    [SerializeField] TextMeshProUGUI DateOfBirthText;
    [SerializeField] TextMeshProUGUI DateOfDeathText;
    [SerializeField] TextMeshProUGUI GenderText;
    [SerializeField] TextMeshProUGUI CauseOfDeathText;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        
    }

    void Start()
    {
        gameObject.SetActive(false);
    }


    public void SetUpFullName(string name)
    {
        if(NameText.text == GhostData.Instance.FullName)
        {
            return;
        }

        NameText.text = "Name:" + name;
    }

    public void SetUpPresenceToggle(bool toggled)
    {
        if (PresenceToggler.isOn)
        {
            return;
        }

        PresenceToggler.isOn = toggled;
    }
    

    public void SetUpAge(string age)
    {
        if(AgeText.text == GhostData.Instance.Age)
        {
            return;
        }

        AgeText.text = "Age:" + age;
    }

    public void SetUpBirth(string birth)
    {
        if(DateOfBirthText.text == GhostData.Instance.DateOfBirth)
        {
            return;
        }

        DateOfBirthText.text = "Date of birth:" + birth;
    }

    public void SetUpDeath(string death)
    {
        if(DateOfDeathText.text == GhostData.Instance.DateOfDeath)
        {
            return;
        }

        DateOfDeathText.text = "Date of death:" + death;
    }


    public void SetUpGender(string gender)
    {
        if(GenderText.text == GhostData.Instance.Gender)
        {
            return;
        }

        GenderText.text = "Gender:" + gender;
    }

    public void SetUpCauseOfDeath(string deathCause)
    {
        if(CauseOfDeathText.text == GhostData.Instance.CauseOfDeath)
        {
            return;
        }

        CauseOfDeathText.text = "Cause Of Death:" + deathCause;
    }

}
