using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public virtual State Tick(AiManager aiManager)
    {
        Debug.Log("Running state");
        return this;
    }
}
