using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    [SerializeField] private List<Questions> allQuestions = new List<Questions>(); //all the possible questions
    [SerializeField] private List<Questions> currentQuestions = new List<Questions>(); // the selected two questions to ask


    int numberOfQuestions = 2;

    [SerializeField]int minWaitingTimeForAResponse = 3;
    [SerializeField]int maxWaitingTimeForAResponse = 10;


    Responses currentResponse;
    Responses starterResponse;


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

        StartGame();
    }


    void PopulateQuestions()
    {
        var _questions = Resources.LoadAll<Questions>("Questions");
        foreach(var q in _questions)
        {
            allQuestions.Add(q);
        }
        
    }

    void StartGame()
    {

        var startQuestions = new List<Questions>();
        foreach(Questions question in allQuestions)
        {
            if(question.Reveals == 0)
            {
                startQuestions.Add(question);
            }
            
        }

        for (int i = 0;  i < numberOfQuestions; i++)
        {
            Questions newQuestion = startQuestions[Random.Range(0, startQuestions.Count)];
            currentQuestions.Add(newQuestion);
            allQuestions.Remove(newQuestion);
            UImanager._instance.SetUpQuestionButton(newQuestion.QuestionText, i);
        }

        UImanager._instance.SetUpResponseText("");
        currentResponse = null;
        starterResponse = null;
        whichReveal = 0;
        startQuestions.Clear();
    }

    void GetTwoQuestions()
    {
        if(allQuestions.Count == 0)
        {
            Debug.LogError("We currently have no questions to ask");
            UImanager._instance.HideOrEnableButtons(true);
            return;
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
        starterResponse = null;
        whichReveal = 0;
        UImanager._instance.HideOrEnableButtons(false);
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
        if(currentQuestions[which].starterResponses.Count > 0)
        {
            starterResponse = currentQuestions[which].starterResponses[Random.Range(0, currentQuestions[which].starterResponses.Count)];
        }
        whichReveal = currentQuestions[which].Reveals;
        AddCurrentQuestions();
        //Debug.Log("Waiting for a response");
        yield return new WaitForSeconds(Random.Range(minWaitingTimeForAResponse, maxWaitingTimeForAResponse));
        GameActions.onAwaitResponse?.Invoke(whichReveal);
    }

    //TODO: Figure out a way to make sure the presence questions also get removed from the list if presence was revealed with a another question(same with age)

    /* Loops throught all the questions in the current questions pool(questions that were selected for the player to ask) and re-adds them
     * to the pool of all questions and then clears the current question pool. This is so all the questions that were already answered can be removed.
     */
    void AddCurrentQuestions()
    {
        foreach(Questions question in currentQuestions)
        {
            allQuestions.Add(question);
        }

        if (currentQuestions.Count > 0)
        {
            //Debug.Log("The list is higher than 0 therefore we need to remove these questions from the list");
            currentQuestions.Clear();

        }
    }


    /*Creates a new list of questions and then checks that list of questions to remove the already answered questions from the 
     * pool of all questions. There probably is a better way of doing this ,but what it works so I don't really care.
     */
    void RemoveAlreadyAskedQuestions()
    {
        List<Questions> q = new List<Questions>();
        foreach(Questions question in allQuestions)
        {
            q.Add(question);
        }

        foreach(Questions _q in q)
        {
            if(_q.Reveals == whichReveal)
            {
                allQuestions.Remove(_q);
            }
        }
        GetTwoQuestions();
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
        RemoveAlreadyAskedQuestions();
    }

    IEnumerator ResponseFailure()
    {
        UImanager._instance.SetUpResponseText(getFullResponse(false));
        yield return new WaitForSeconds(2f);
        UImanager._instance.HideOrEnableButtons(false);
        UImanager._instance.SetUpResponseText("");
        GetTwoQuestions();
    }


    //This current way of making dynamic responses is dumb. I need to find a better way to do this ,but currently it works so it's whatever
    string getFullResponse(bool isSuccessful)
    {
        if (isSuccessful)
        {
            switch (whichReveal)
            {
                case 0:
                    if(starterResponse == null)
                    {
                        return currentResponse.ResponseText;
                    }
                    else
                    {
                        return "";
                    }
                    
                case 1:
                    if(starterResponse == null)
                    {
                        return currentResponse.ResponseText + GhostData.Instance.FullName;
                    }
                    else
                    {
                        return "";
                    }
                case 2:
                    if (starterResponse == null)
                    {
                        return GhostData.Instance.Age + currentResponse.ResponseText;
                    }
                    else
                    {
                        return starterResponse.ResponseText + GhostData.Instance.Age + currentResponse.ResponseText;
                    }
                case 3:
                    if (starterResponse == null)
                    {
                        return currentResponse.ResponseText + GhostData.Instance.DateOfBirth;
                    }
                    else
                    {
                        return "";
                    }
                case 4:
                    if (starterResponse == null)
                    {
                        return currentResponse.ResponseText + GhostData.Instance.DateOfDeath;
                    }
                    else
                    {
                        return "";
                    }
            }
        }

        return "No response";
        
    }
    
}
