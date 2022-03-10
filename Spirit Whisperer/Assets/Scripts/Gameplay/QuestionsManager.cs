using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{

    public static int currentOrder { get; private set; }
    public static bool WaitingForAResponse { get; private set; }


    [SerializeField] List<Questions> allQuestions = new List<Questions>(); //all the possible questions
    [SerializeField] List<Questions> currentQuestions = new List<Questions>(); // the selected two questions to ask

    List<Questions> inOrderQuestions = new List<Questions>();
    List<Questions> possibleQuestionsToAsk = new List<Questions>();

    int numberOfQuestions = 2;

    [SerializeField]int minWaitingTimeForAResponse = 3;
    [SerializeField]int maxWaitingTimeForAResponse = 10;


    Responses currentResponse;
    Responses starterResponse;


    int whichReveal;
    int whichButtonIsRandom;
    bool previousQuestionNotInOrder;
    bool ResponseWasASuccess;
    int previousOrder;

    void OnEnable()
    {
        GameActions.onButtonPress += StartResponseWait;
        GameActions.onResponseSucceded += OnResponseSuccess;
        GameActions.onResponseFailed += OnResponseFailed;
        GameActions.onQuestionInOrder += onQuestionInOrder;
        GameActions.onInsideRadiusOfGhost += CancelResponseIfGhostIsNotInRange;
        GameActions.onHuntStart += CancelResponseOnHunt;
    }

    void OnDisable()
    {
        GameActions.onButtonPress -= StartResponseWait;
        GameActions.onResponseSucceded -= OnResponseSuccess;
        GameActions.onResponseFailed -= OnResponseFailed;
        GameActions.onQuestionInOrder -= onQuestionInOrder;
        GameActions.onInsideRadiusOfGhost -= CancelResponseIfGhostIsNotInRange;
        GameActions.onHuntStart -= CancelResponseOnHunt;
    }


    void Awake()
    {
        PopulateQuestions();
    }

    void Start()
    {
        currentOrder = 0;
        previousOrder = currentOrder;
        WaitingForAResponse = false;
        previousQuestionNotInOrder = false;
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
            allQuestions.Remove(newQuestion);
            currentQuestions.Add(newQuestion);
            UImanager._instance.SetUpQuestionButton(newQuestion.QuestionText, i);
        }

        UImanager._instance.SetUpResponseText("");
        currentResponse = null;
        starterResponse = null;
        whichReveal = 0;
        startQuestions.Clear();
    }

    void GetTwoQuestions(bool canceledResponse)
    {
        if(allQuestions.Count == 0)
        {
            Debug.LogError("We currently have no questions to ask");
            UImanager._instance.HideOrEnableButtons(true);
            return;
        }

        if (!canceledResponse)
        {
            if (!previousQuestionNotInOrder && ResponseWasASuccess)
            {
                currentOrder++;
                
            }
        }
        else
        {
            if(currentOrder != previousOrder)
            {
                currentOrder = previousOrder;
            }
        }
        Debug.Log("current order: " + currentOrder);
        inOrderQuestions.Clear();
        PopulatePossibleQuestionsPool();
        Questions newQuestion;
        Questions newInOrderQuestion;

        foreach (Questions question in allQuestions)
        {
            if(question.Reveals == currentOrder)
            {
                inOrderQuestions.Add(question);
                possibleQuestionsToAsk.Remove(question);
            }
        }

        whichButtonIsRandom = Random.Range(0, 2);


        for (int i = 0; i < numberOfQuestions; i++)
        {
            if(whichButtonIsRandom == i)
            {
                newInOrderQuestion = inOrderQuestions[Random.Range(0, inOrderQuestions.Count)];
                allQuestions.Remove(newInOrderQuestion);
                currentQuestions.Add(newInOrderQuestion);
                UImanager._instance.SetUpQuestionButton(newInOrderQuestion.QuestionText, whichButtonIsRandom);
            }
            else
            {
                
                if (possibleQuestionsToAsk.Count > 0)
                {
                    
                   newQuestion = possibleQuestionsToAsk[Random.Range(0, possibleQuestionsToAsk.Count)];
                }
                else
                {
                    newQuestion = allQuestions[Random.Range(0, allQuestions.Count)];
                }
                
                allQuestions.Remove(newQuestion);
                currentQuestions.Add(newQuestion);
                UImanager._instance.SetUpQuestionButton(newQuestion.QuestionText, i);
            }
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
        WaitingForAResponse = true;
    }


    IEnumerator WaitForResponse(int which)
    {
        UImanager._instance.HideOrEnableButtons(true);
        GameActions.onDisableToggleButton?.Invoke(false);
        //Caching the current response because the entire line of code is too long 
        if (currentQuestions[which].responses.Count > 0)
        {
            currentResponse = currentQuestions[which].responses[Random.Range(0, currentQuestions[which].responses.Count)];
        }
        
        if(currentQuestions[which].starterResponses.Count > 0)
        {
            starterResponse = currentQuestions[which].starterResponses[Random.Range(0, currentQuestions[which].starterResponses.Count)];
        }
        whichReveal = currentQuestions[which].Reveals;
        previousOrder = currentOrder;
        AddCurrentQuestions();
        //Debug.Log("Waiting for a response");
        yield return new WaitForSeconds(Random.Range(minWaitingTimeForAResponse, maxWaitingTimeForAResponse));
        GameActions.onAwaitResponse?.Invoke(whichReveal);
        
    }

    

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
        GetTwoQuestions(false);
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
        ResponseWasASuccess = true;
        UImanager._instance.SetUpResponseText(getFullResponse());
        yield return new WaitForSeconds(5f);
        RemoveAlreadyAskedQuestions();
        GameActions.onDisableToggleButton?.Invoke(true);
        WaitingForAResponse = false;
    }

    IEnumerator ResponseFailure()
    {
        ResponseWasASuccess = false;
        UImanager._instance.SetUpResponseText(getFullResponse());
        yield return new WaitForSeconds(2f);
        UImanager._instance.HideOrEnableButtons(false);
        UImanager._instance.SetUpResponseText("");
        GameActions.onDisableToggleButton?.Invoke(true);
        GetTwoQuestions(false);
        WaitingForAResponse = false;
    }


    //This current way of making dynamic responses is dumb. I need to find a better way to do this ,but currently it works so it's whatever
    string getFullResponse()
    {
        if (ResponseWasASuccess)
        {
            switch (whichReveal)
            {
                case 0:
                    if(starterResponse == null && currentResponse != null)
                    {
                        return currentResponse.ResponseText;
                    }
                    else
                    {
                        return "";
                    }
                    
                case 1:
                    if(starterResponse == null && currentResponse != null)
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
                        if(currentResponse != null)
                        {
                            return GhostData.Instance.Age + currentResponse.ResponseText;
                        }
                        return "";
                    }
                    else
                    {
                        return starterResponse.ResponseText + GhostData.Instance.Age + currentResponse.ResponseText;
                    }
                case 3:
                    if (starterResponse == null && currentResponse != null)
                    {
                        return currentResponse.ResponseText + GhostData.Instance.Gender;
                    }
                    else
                    {
                        return "";
                    }
                case 4:
                    if (starterResponse == null && currentResponse != null)
                    {
                        return currentResponse.ResponseText + GhostData.Instance.DateOfBirth;
                    }
                    else
                    {
                        return "";
                    }
                case 5:
                    if(starterResponse == null && currentResponse != null)
                    {
                        return currentResponse.ResponseText + GhostData.Instance.DateOfDeath;
                    }
                    else
                    {
                        return "";
                    }
                case 6:
                    if(starterResponse == null && currentResponse == null)
                    {
                        return GhostData.Instance.CauseOfDeath;
                    }
                    else
                    {
                        return "";
                    }
            }
        }

        return "No response";
        
    }

    void onQuestionInOrder(bool inOrder)
    {
        previousQuestionNotInOrder = inOrder;
    }

    void PopulatePossibleQuestionsPool()
    {
        possibleQuestionsToAsk.Clear();
        foreach(Questions q in allQuestions)
        {
            possibleQuestionsToAsk.Add(q);
        }
    }


    void CancelResponseIfGhostIsNotInRange(bool ghostInRange)
    {

        if (!WaitingForAResponse && ghostInRange)
        {
            //Debug.Log("Despite the ghost being in range, you're not waiting on a response. No need to check any futher");
            return;
        }



        if(!ghostInRange && WaitingForAResponse)
        {
            //Debug.LogWarning("The ghost left the spirit box range while you were wating for a response. Canceling response");
            StopAllCoroutines();
            WaitingForAResponse = false;
            UImanager._instance.HideOrEnableButtons(false);
            UImanager._instance.SetUpResponseText("");
            GameActions.onDisableToggleButton?.Invoke(true);
            GetTwoQuestions(true);
        }
        else if(ghostInRange && WaitingForAResponse)
        {
            return;
        }
    }


    void CancelResponseOnHunt()
    {
        if (!WaitingForAResponse)
        {
            return;
        }


        Debug.Log("A hunt has started");

        StopAllCoroutines();
        WaitingForAResponse = false;
        UImanager._instance.HideOrEnableButtons(false);
        UImanager._instance.SetUpResponseText("");
        GameActions.onDisableToggleButton?.Invoke(true);
        GetTwoQuestions(true);
        
    }
    
}
