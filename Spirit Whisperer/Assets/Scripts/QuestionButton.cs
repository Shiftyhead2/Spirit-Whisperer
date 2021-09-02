using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionButton : MonoBehaviour
{

    [SerializeField] int whichButton;

    public void PressButton()
    {
        GameActions.onButtonPress?.Invoke(whichButton);
    }
    
}
