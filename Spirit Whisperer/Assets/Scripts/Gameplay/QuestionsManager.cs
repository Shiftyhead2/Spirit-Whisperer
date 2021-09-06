using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    [SerializeField] private List<Questions> allQuestions = new List<Questions>();
    [SerializeField] private List<Questions> currentQuestions = new List<Questions>();

    int numberOfQuestions = 2;

    [SerializeField]int minWaitingTimeForAResponse = 3;
    [SerializeField]int maxWaitingTimeForAResponse = 10;


    Responses currentResponse;


    int whichReveal;

    void OnEnable()
    {
        GameActions.onButtonPress += StartResponseWait;
        GameActions.onResponseSucceded += OnResponseSuccess;
        GameActions.onResponseFailed += OnResponseFailed;
    }

    void OnDisable()
    {
        GameActions.onButtonPress -= StartResponseWait;
        GameActions.onResponseSucceded -= OnResponseSuccess;
        GameActions.onResponseFailed -= OnResponseFailed;
    }

    void Awake()
    {
        PopulateQuestions();
        
    }

    void Start()
    {
        GetTwoQuestions();
    }


    void PopulateQuestions()
    {
        var _questions = Resources.LoadAll<Questions>("Questions");
        foreach(var q in _questions)
        {
            allQuestions.Add(q);
        }
    }

    void GetTwoQuestions()
    {
        if(currentQuestions.Count > 0)
        {
            currentQuestions.Clear();
        }


        for (int i = 0; i < numberOfQuestions; i++)
        {
            Questions newQuestion = allQuestions[Random.Range(0, allQuestions.Count)];
            currentQuestions.Add(newQuestion);
            allQuestions.Remove(newQuestion);
            UImanager._instance.SetUpQuestionButton(newQuestion.QuestionText, i);
        }
        UImanager._instance.SetUpResponseText("");
        currentResponse = null;
        whichReveal = 0;
    }


    void StartResponseWait(int which)
    {
        StopAllCoroutines();
        StartCoroutine(WaitForResponse(which));
    }


    IEnumerator WaitForResponse(int which)
    {
        UImanager._instance.HideOrEnableButtons(true);
        //Caching the current response because the entire line of code is too long 
        currentResponse = currentQuestions[which].responses[Random.Range(0, currentQuestions[which].responses.Count)];
        whichReveal = currentQuestions[which].Reveals;
        Debug.Log("Waiting for a response");
        yield return new WaitForSeconds(Random.Range(minWaitingTimeForAResponse, maxWaitingTimeForAResponse));
        GameActions.onAwaitResponse?.Invoke(currentQuestions[which].Reveals);
    }

    //TODO: later down the line this function will enable removal of all the questions whos type has already been answered too

    void AddCurrentQuestions()
    {
        foreach(Questions question in currentQuestions)
        {
            allQuestions.Add(question);
        }
    }

    void OnResponseSuccess()
    {
        StopAllCoroutines();
        StartCoroutine(ResponseSuccess());
    }


    void OnResponseFailed()
    {
        StopAllCoroutines();
        StartCoroutine(ResponseFailure());
       
    }

    IEnumerator ResponseSuccess()
    {
        UImanager._instance.SetUpResponseText(getFullResponse(true));
        yield return new WaitForSeconds(5f);
        AddCurrentQuestions();
        UImanager._instance.HideOrEnableButtons(false);
        GetTwoQuestions();
    }

    IEnumerator ResponseFailure()
    {
        UImanager._instance.SetUpResponseText(getFullResponse(false));
        yield return new WaitForSeconds(2f);
        UImanager._instance.HideOrEnableButtons(false);
        UImanager._instance.SetUpResponseText("");
    }

    string getFullResponse(bool isSuccessful)
    {
        if (isSuccessful)
        {
            switch (whichReveal)
            {
                case 0:
                    return currentResponse.ResponseText;
                case 1:
                    return currentResponse.ResponseText + GhostData.Instance.FullName;
            }
        }

        return "No response";
        
    }
    
}
