using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInformationPanel : MonoBehaviour
{
    public GameObject InformationPanel;
    public GameObject[] PanelsToEnable;

    public Button InformationPanelButton;

    bool isInformationEnabled = false;
    bool arePanelsEnabled = false;

    void OnEnable()
    {
        GameActions.onDisableToggleButton += ToggleInformationPanelButton;
        GameActions.onToggleInformation += Toggle;
        GameActions.onTogglePanels += TogglePanels;
    }

    void OnDisable()
    {
        GameActions.onDisableToggleButton -= ToggleInformationPanelButton;
        GameActions.onToggleInformation -= Toggle;
        GameActions.onTogglePanels -= TogglePanels;
    }


    private void Start()
    {
        for (int i = 0; i < PanelsToEnable.Length; i++)
        {
            PanelsToEnable[i].SetActive(arePanelsEnabled);
        }
    }

    public void Toggle()
    {
        if (InformationPanelButton.interactable == false)
        {
            return;
        }

        isInformationEnabled = !isInformationEnabled;
        InformationPanel.SetActive(isInformationEnabled);


        if (InformationPanel.activeInHierarchy == false) 
        {
            for (int i = 0; i < PanelsToEnable.Length; i++)
            {
                if (arePanelsEnabled)
                {
                    PanelsToEnable[i].SetActive(true);
                }
                else
                {
                    return;
                }
                
            }
        }
        else
        {
            for (int i = 0; i < PanelsToEnable.Length; i++)
            {
                if (arePanelsEnabled)
                {
                    PanelsToEnable[i].SetActive(false);
                }
                else
                {
                    return;
                }
                
            }
        }
        
    }

    void ToggleInformationPanelButton(bool enabled)
    {
        InformationPanelButton.interactable = enabled;
    }


    void TogglePanels()
    {
        if (InformationPanel.activeInHierarchy || InformationPanelButton.interactable == false || QuestionsManager.WaitingForAResponse)
        {
            return;
        }

        arePanelsEnabled = !arePanelsEnabled;
        for (int i = 0; i < PanelsToEnable.Length; i++)
        {
            PanelsToEnable[i].SetActive(arePanelsEnabled);
        }
    }

    
}
