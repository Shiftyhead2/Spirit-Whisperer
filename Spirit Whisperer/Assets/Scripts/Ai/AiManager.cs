using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiManager : MonoBehaviour
{
    [Header("Current state")]
    public IdleState startingState;
    private State currentState;

    [Header("Current target")]
    public Transform currentTarget;
    public float distanceFromCurrentTarget;

    public Animator animator;
    public NavMeshAgent AINavMeshAgent;

    [Header("Rigidbody")]
    public Rigidbody AIrigidbody;

    [Header("Stopping Distances")]
    public float minimumAttackDistance = 2f;
    public float minimumWanderStoppingDistance = 0.2f;

    private void Awake()
    {
        currentState = startingState;
        AINavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AIrigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        animator.SetFloat("vertical", AINavMeshAgent.velocity.magnitude);

        if(currentTarget != null)
        {
            distanceFromCurrentTarget = Vector3.Distance(currentTarget.position, transform.position);
        }
    }

    private void FixedUpdate()
    {
        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        State nextState;
        if(currentState != null)
        {
            nextState = currentState.Tick(this);
            if (nextState != null)
            {
                currentState = nextState;
            }
        }
    }      
}
