using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PursuitState pursueTargetState;
    FieldOfViewScript FOV;
    WanderState wanderState;

    [Header("Wander Time")]
    public float wanderTime = 1f;
    float currentWanderTime;
    

    private void Awake()
    {
        pursueTargetState = GetComponent<PursuitState>();
        FOV = GetComponentInParent<FieldOfViewScript>();
        wanderState = GetComponent<WanderState>();
    }


    private void OnEnable()
    {
        GameActions.onHuntStart += ResetFlags;
        GameActions.onHuntEnd += ResetFlags;
    }

    private void OnDisable()
    {
        GameActions.onHuntStart -= ResetFlags;
        GameActions.onHuntEnd -= ResetFlags;
    }


    public override State Tick(AiManager aiManager)
    {
        FOV.FindATargetViaLineOfSight();
        if (aiManager.currentTarget != null)
        {
            //Debug.Log("We have found a target");
            ResetFlags();
            return pursueTargetState;
        }
        else
        {
            if(currentWanderTime >= wanderTime)
            {
                ResetFlags();
                return wanderState;
            }
            else
            {
                currentWanderTime += Time.deltaTime;
            }


            return this;
        }
    } 
    

    void ResetFlags()
    {
        currentWanderTime = 0f;
    }
}
