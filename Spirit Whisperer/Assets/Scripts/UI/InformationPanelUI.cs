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
        NameText.text = "Name:" + name;
    }

    public void SetUpPresenceToggle(bool toggled)
    {
        PresenceToggler.isOn = toggled;
    } 

}
