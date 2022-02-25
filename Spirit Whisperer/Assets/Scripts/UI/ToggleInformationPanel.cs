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
        GameActions.onHuntStart += HidePanels;
    }

    void OnDisable()
    {
        GameActions.onDisableToggleButton -= ToggleInformationPanelButton;
        GameActions.onToggleInformation -= Toggle;
        GameActions.onTogglePanels -= TogglePanels;
        GameActions.onHuntStart -= HidePanels;
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
            InformationRectTransform.DOAnchorPos(InformationPanelOpenPosition, 0.5f);
        }
        else
        {
            InformationRectTransform.DOAnchorPos(InformationPanelClosedPosition, 0.5f);
        }
    }


    void TogglePanels()
    {
       
        if (isInformationEnabled || InformationPanelButton.interactable == false || QuestionsManager.WaitingForAResponse || GameManager.isHuntActivated)
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
            ResponseRectTransform.DOAnchorPos(ResponsePanelOpenPosition, 0.5f).OnComplete(() => GameActions.onShowButtons?.Invoke());
        }
        else
        {
            ResponseRectTransform.DOAnchorPos(ResponsePanelClosedPosition, 0.5f).OnStart(() => GameActions.onShowButtons?.Invoke());
        }
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


    void HidePanels()
    {
        if (arePanelsEnabled)
        {
            arePanelsEnabled = false;
            TweenResponsePanels(false);
        }
    }

    
}
