using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{


    FieldOfViewScript FOV;
    IdleState idleState;
    PursuitState pursuitState;

    bool hasTarget = false;

    [Header("Wander Settings")]
    public Bounds boundBox;

    private Vector3 targetPos;

    float targetDistance;


    private void Awake()
    {
        pursuitState = GetComponent<PursuitState>();
        FOV = GetComponentInParent<FieldOfViewScript>();
        idleState = GetComponent<IdleState>();
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
                targetDistance = Vector3.Distance(targetPos, aiManager.transform.position);

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
        targetPos = GetRandomPoint(aiManager);
        aiManager.AINavMeshAgent.isStopped = false;
        hasTarget = true;
        aiManager.AINavMeshAgent.SetDestination(targetPos);
    }

    Vector3 GetRandomPoint(AiManager aiManager)
    {
        float randomX = Random.Range(-boundBox.extents.x + aiManager.AINavMeshAgent.radius, boundBox.extents.x - aiManager.AINavMeshAgent.radius);
        float randomZ = Random.Range(-boundBox.extents.z + aiManager.AINavMeshAgent.radius, boundBox.extents.z - aiManager.AINavMeshAgent.radius);
        return new Vector3(randomX, aiManager.transform.position.y, randomZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boundBox.center, boundBox.size);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.2f);
    }

}

