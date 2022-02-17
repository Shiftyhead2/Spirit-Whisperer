using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiManager : MonoBehaviour
{

    public bool IsWandering = false;

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

    [Header("Locomotion")]
    public float rotationSpeed = 5f;

    [Header("Attack")]
    public float minimumDistanceToAttack = 1f;

    private void Awake()
    {
        currentState = startingState;
        AINavMeshAgent = GetComponentInChildren<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AIrigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        AINavMeshAgent.transform.localPosition = Vector3.zero;

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
