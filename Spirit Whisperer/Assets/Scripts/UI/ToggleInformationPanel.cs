using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleInformationPanel : MonoBehaviour
{
    public GameObject InformationPanel;

    public void Toggle()
    {
        if (InformationPanel.activeInHierarchy == false) 
        {
            InformationPanel.SetActive(true);
        }
        else
        {
            InformationPanel.SetActive(false);
        }
        
    }
}
