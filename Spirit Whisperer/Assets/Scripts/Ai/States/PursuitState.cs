using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitState : State
{

    AttackState attackState;

    private void Awake()
    {
        attackState = GetComponent<AttackState>();
    }

    public override State Tick(AiManager aiManager)
    {
        //Debug.Log("We are in the pursuit state");
        MoveTowardsCurrentTarget(aiManager);

        if (aiManager.distanceFromCurrentTarget <= aiManager.minimumAttackDistance)
        {
            return attackState;
        }
        else
        {
            return this;
        }
        
    }

    private void MoveTowardsCurrentTarget(AiManager aiManager)
    {
        aiManager.AINavMeshAgent.enabled = true;
        aiManager.AINavMeshAgent.isStopped = false;
        aiManager.AINavMeshAgent.SetDestination(aiManager.currentTarget.position);
    }
}
