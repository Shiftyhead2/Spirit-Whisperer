using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBoxRadiusChecker : MonoBehaviour
{
    public float detectionDistanceMax;
    [SerializeField] private float repeatInterval;
    public bool ghostInRange { get; private set; } = false;

    public GameObject ghost { get; private set; }
    private float currentDistance;


    private void Start()
    {
        ghost = GameObject.FindGameObjectWithTag("AI");
        ghostInRange = false;
        InvokeMethod();
    }


    void OnEnable()
    {
        GameActions.onHuntStart += OnHuntStart;
        GameActions.onHuntEnd += OnHuntEnd;
        GameActions.onJumpScare += onJumpScare;
    }

    void OnDisable()
    {
        GameActions.onHuntStart -= OnHuntStart;
        GameActions.onHuntEnd -= OnHuntEnd;
        GameActions.onJumpScare -= onJumpScare;
    }


    void InvokeMethod()
    {
        StartCoroutine(CheckRadius());
    }



    private IEnumerator CheckRadius()
    {
        WaitForSeconds wait = new WaitForSeconds(repeatInterval);

        while (true)
        {
            yield return wait;
            IsGhostInRange();
        }
    }


    void OnHuntStart()
    {
        StopAllCoroutines();
        if (ghostInRange)
        {
            ghostInRange = false;
        }
    }


    void OnHuntEnd()
    {
        InvokeMethod();
    }


    void onJumpScare()
    {
        StopAllCoroutines();
    }



    private void IsGhostInRange()
    {
        currentDistance = Vector3.Distance(transform.position, ghost.transform.position);

        if(currentDistance <= detectionDistanceMax)
        {
            if (!ghostInRange)
            {
                ghostInRange = true;
                GameActions.onInsideRadiusOfGhost?.Invoke(true);
            }
        }
        else
        {
            if (ghostInRange)
            {
                ghostInRange = false;
            }
            GameActions.onInsideRadiusOfGhost?.Invoke(false);
        }
    }
}
