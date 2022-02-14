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
        Debug.Log("We are in the pursuit state");
        MoveTowardsCurrentTarget(aiManager);
        RotateTowardsCurrentTarget(aiManager);

        if (aiManager.distanceFromCurrentTarget <= aiManager.minimumDistanceToAttack)
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
        aiManager.animator.SetFloat("vertical", 2, 0.2f, Time.deltaTime);
    }

    private void RotateTowardsCurrentTarget(AiManager aiManager)
    {
        aiManager.AINavMeshAgent.enabled = true;
        aiManager.AINavMeshAgent.SetDestination(aiManager.currentTarget.position);
        aiManager.transform.rotation = Quaternion.Slerp(aiManager.transform.rotation, aiManager.AINavMeshAgent.transform.rotation,aiManager.rotationSpeed / Time.deltaTime);
    }
}
