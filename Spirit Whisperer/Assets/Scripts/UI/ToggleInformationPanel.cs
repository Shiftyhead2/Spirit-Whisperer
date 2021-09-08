using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInformationPanel : MonoBehaviour
{
    public GameObject InformationPanel;
    public GameObject ResponsePanel;
    public GameObject QuestionsPanel;

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
}
