using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionButton : MonoBehaviour
{

    [SerializeField] int whichButton;
    bool isShown = false;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
        isShown = false;
    }

    private void OnEnable()
    {
        GameActions.onQuestionAsked += PressButton;
        GameActions.onInsideRadiusOfGhost += ToggleButton;
        GameActions.onShowButtons += ToggleShown;
        GameActions.onHuntStart += OnHuntStart;
        GameActions.onHuntEnd += OnHuntEnd;
    }

    private void OnDisable()
    {
        GameActions.onQuestionAsked -= PressButton;
        GameActions.onInsideRadiusOfGhost -= ToggleButton;
        GameActions.onShowButtons -= ToggleShown;
        GameActions.onHuntStart -= OnHuntStart;
        GameActions.onHuntEnd -= OnHuntEnd;
    }

    public void PressButton(int which)
    {
        if (isShown && button.interactable && which == whichButton)
        {
            GameActions.onButtonPress?.Invoke(whichButton);
        }
        else
        {
            return;
        }
    }

    private void OnHuntStart()
    {
        ToggleButton(false);
    }


    private void OnHuntEnd()
    {
        ToggleButton(true);
    }


    private void ToggleButton(bool enabled)
    {
        button.interactable = enabled;
    }

    private void ToggleShown()
    {
        isShown = !isShown;
    }
    
}
