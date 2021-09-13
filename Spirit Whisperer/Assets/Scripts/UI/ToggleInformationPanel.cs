using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInformationPanel : MonoBehaviour
{
    public GameObject InformationPanel;
    public GameObject ResponsePanel;
    public GameObject QuestionsPanel;

    public Button InformationPanelButton;

    void OnEnable()
    {
        GameActions.onDisableToggleButton += ToggleInformationPanelButton;
    }

    void OnDisable()
    {
        GameActions.onDisableToggleButton -= ToggleInformationPanelButton;
    }

    public void Toggle()
    {
        if (InformationPanel.activeInHierarchy == false) 
        {
            InformationPanel.SetActive(true);
            ResponsePanel.SetActive(false);
            QuestionsPanel.SetActive(false);
        }
        else
        {
            InformationPanel.SetActive(false);
            ResponsePanel.SetActive(true);
            QuestionsPanel.SetActive(true);
        }
        
    }

    void ToggleInformationPanelButton(bool enabled)
    {
        InformationPanelButton.interactable = enabled;
    }

    
}
