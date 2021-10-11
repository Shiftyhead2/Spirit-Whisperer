using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButton : MonoBehaviour
{

    [SerializeField] int whichButton;

    private void OnEnable()
    {
        GameActions.onQuestionAsked += PressButton;
    }

    private void OnDisable()
    {
        GameActions.onQuestionAsked -= PressButton;
    }

    public void PressButton(int which)
    {
        if (gameObject.activeInHierarchy && which == whichButton)
        {
            GameActions.onButtonPress?.Invoke(whichButton);
        }
        else
        {
            return;
        }
    }
    
}
