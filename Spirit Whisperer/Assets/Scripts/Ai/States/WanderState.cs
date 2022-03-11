using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{


    FieldOfViewScript FOV;
    IdleState idleState;
    PursuitState pursuitState;
    WanderBounds wanderBounds;

    bool hasTarget = false;

    float targetDistance;


    private void Awake()
    {
        pursuitState = GetComponent<PursuitState>();
        FOV = GetComponentInParent<FieldOfViewScript>();
        idleState = GetComponent<IdleState>();
        wanderBounds = GetComponentInParent<WanderBounds>();
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
        //Debug.Log("We are in the wander state");
        FOV.FindATargetViaLineOfSight();
        if(aiManager.currentTarget != null)
        {
            ResetFlags();
            return pursuitState;
        }
        else
        {

            if (!hasTarget)
            {
                FindNewTargetPosition(aiManager);
            }
            else
            {
                targetDistance = Vector3.Distance(wanderBounds.targetPos, aiManager.transform.position);

                if(targetDistance <= aiManager.minimumWanderStoppingDistance)
                {
                    ResetFlags();
                    return idleState;
                }
                
            }

            return this;
            
        }
        
    }

    void ResetFlags()
    {
        hasTarget = false;
        targetDistance = 0f;
    }


    void FindNewTargetPosition(AiManager aiManager)
    {
        wanderBounds.SetTargetPos(aiManager);
        aiManager.AINavMeshAgent.isStopped = false;
        hasTarget = true;
        aiManager.AINavMeshAgent.SetDestination(wanderBounds.targetPos);
    } 
}

