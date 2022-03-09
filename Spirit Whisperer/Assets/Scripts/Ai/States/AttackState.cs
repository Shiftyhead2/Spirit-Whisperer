using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override State Tick(AiManager aiManager)
    {
        aiManager.AINavMeshAgent.isStopped = true;
        JumpScare(aiManager);
        return this;
    }


    void JumpScare(AiManager aiManager)
    {
        if(aiManager.isHunting && GameManager.isJumpscared == false)
        {
            GameActions.onJumpScare?.Invoke();
            aiManager.animator.SetTrigger("jumpscare");
        }
    }
}
