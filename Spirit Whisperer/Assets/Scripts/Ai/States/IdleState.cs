using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    PursuitState pursueTargetState;
    FieldOfViewScript FOV;
    

    private void Awake()
    {
        pursueTargetState = GetComponent<PursuitState>();
        FOV = GetComponentInParent<FieldOfViewScript>();
    }


    public override State Tick(AiManager aiManager)
    {
        FOV.FindATargetViaLineOfSight();
        if (aiManager.currentTarget != null)
        {
            //Debug.Log("We have found a target");
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }    
}
