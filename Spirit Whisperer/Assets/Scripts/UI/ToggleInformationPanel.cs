using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/* Handles toggling various gameplay panels(Response panel, Information panel)*/

public class ToggleInformationPanel : MonoBehaviour
{
    
    [Header("UI panels")]
    public GameObject InformationPanel;
    public GameObject ResponsePanel;


    public RectTransform ResponseRectTransform, InformationRectTransform;

    [Header("UI buttons")]
    public Button InformationPanelButton;
    public Button[] questionButtons;

    bool isInformationEnabled = false;
    bool arePanelsEnabled = false;

    [Header("Information Panel variables")]
    [SerializeField]
    private Vector2 InformationPanelOpenPosition = new Vector2(0, -6);
    [SerializeField]
    private Vector2 InformationPanelClosedPosition = new Vector2(0,-1264);

    [Header("Response Panel Variables")]
    [SerializeField]
    private Vector2 ResponsePanelOpenPosition = new Vector2(0, 212);
    [SerializeField]
    private Vector2 ResponsePanelClosedPosition = new Vector2(0, -301);

    

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
        ResponsePanel.SetActive(arePanelsEnabled);
    }

    public void Toggle()
    {
        if (InformationPanelButton.interactable == false)
        {
            return;
        }

        isInformationEnabled = !isInformationEnabled;

        TweenInformationPanel();
        ShowInformationPanels();
  
    }

    void ToggleInformationPanelButton(bool enabled)
    {
        InformationPanelButton.interactable = enabled;
    }


    void TweenInformationPanel()
    {
        if (isInformationEnabled)
        {
            InformationRectTransform.DOAnchorPos(InformationPanelOpenPosition, 0.5f).OnStart(ShowInformationPanel);
        }
        else
        {
            InformationRectTransform.DOAnchorPos(InformationPanelClosedPosition, 0.5f).OnComplete(ShowInformationPanel);
        }
    }


    void TogglePanels()
    {
       
        if (isInformationEnabled || InformationPanelButton.interactable == false || QuestionsManager.WaitingForAResponse)
        {
            return;
        }

        arePanelsEnabled = !arePanelsEnabled;
        TweenResponsePanels(arePanelsEnabled);
        
    }

    void TweenResponsePanels(bool active)
    {
        if (active)
        {
            ResponseRectTransform.DOAnchorPos(ResponsePanelOpenPosition, 0.5f).OnStart(() => ToggleResponsePanels(active)).OnComplete(() => ToggleQuestionButtons(active));
        }
        else
        {
            ResponseRectTransform.DOAnchorPos(ResponsePanelClosedPosition, 0.5f).OnStart(() => ToggleQuestionButtons(active)).OnComplete(() => ToggleResponsePanels(active));
        }
    }

    void ToggleQuestionButtons(bool active)
    {
        for (int i = 0; i < questionButtons.Length; i++)
        {
            questionButtons[i].gameObject.SetActive(active);
        }
    }


    void ToggleResponsePanels(bool active)
    {
        ResponsePanel.SetActive(active);
    }

    void ShowInformationPanel()
    {
        InformationPanel.SetActive(isInformationEnabled);
    }

    void ShowInformationPanels()
    {
        if (isInformationEnabled)
        {
            if (arePanelsEnabled)
            {
                arePanelsEnabled = false;
                TweenResponsePanels(false);
            }
            else
            {
                return;
            }
        }
    }

    
}
