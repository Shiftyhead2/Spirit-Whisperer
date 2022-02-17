using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override State Tick(AiManager aiManager)
    {
        aiManager.animator.SetFloat("vertical", 0, 0.2f, Time.deltaTime);
        return this;
    }
}
