using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* Sets up the questions UI and the response UI */

public class UImanager : MonoBehaviour
{

    public static UImanager _instance{ get; private set; }

    [Header("UI")]
    [SerializeField] Button[] questionButtons;
    [SerializeField] TextMeshProUGUI responseText;


    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    
    public void SetUpQuestionButton(string text, int which)
    {
        for (int i = 0; i < questionButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = questionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if(i == which)
            {
                buttonText.text = text;
            }
        }
    }

    public void SetUpResponseText(string text)
    {
        responseText.text = text.ToUpper();
    }


    public void HideOrEnableButtons(bool disable)
    {
        for (int i = 0; i < questionButtons.Length; i++)
        {
            questionButtons[i].gameObject.SetActive(!disable);
        }
    }

}
