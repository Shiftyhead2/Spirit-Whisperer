using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiManager : MonoBehaviour
{
    [Header("Current state")]
    public WanderState startingState;
    private State currentState;

    [Header("Current target")]
    public Transform currentTarget;
    public float distanceFromCurrentTarget;

    public Animator animator;
    public NavMeshAgent AINavMeshAgent;

    [Header("Rigidbody")]
    public Rigidbody AiRigidbody;

    [Header("Stopping Distances")]
    public float minimumAttackDistance = 2f;
    public float minimumWanderStoppingDistance = 0.2f;

    bool isHunting = false;

    Collider AiCollider;
    SkinnedMeshRenderer meshRenderer;

    private void Awake()
    {
        currentState = startingState;
        AINavMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        AiRigidbody = GetComponent<Rigidbody>();
        AiCollider = GetComponent<CapsuleCollider>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        isHunting = false;
        EnableMiscStuff();
    }

    void OnEnable()
    {
        GameActions.onHuntStart += OnHuntStart;
        GameActions.onHuntEnd += OnHuntEnd;
    }

    void OnDisable()
    {
        GameActions.onHuntStart -= OnHuntStart;
        GameActions.onHuntEnd -= OnHuntEnd;
    }

    private void Update()
    {
        if (isHunting)
        {
            animator.SetFloat("vertical", AINavMeshAgent.velocity.magnitude);

            if (currentTarget != null)
            {
                distanceFromCurrentTarget = Vector3.Distance(currentTarget.position, transform.position);
            }
        }
    }

    private void FixedUpdate()
    {
        if (isHunting)
        {
            HandleStateMachine();
        }
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
    

    private void EnableMiscStuff()
    {
        if (isHunting)
        {
            AiCollider.isTrigger = false;
            meshRenderer.enabled = true;
            AINavMeshAgent.isStopped = false;
            animator.enabled = true;
        }
        else
        {
            AiCollider.isTrigger = true;
            meshRenderer.enabled = false;
            AINavMeshAgent.isStopped = true;
            animator.enabled = false;
        }
    }


    void OnHuntStart()
    {
        currentState = startingState;
        HandleHuntState(true);
    }

    void OnHuntEnd()
    {
        currentState = startingState;
        currentTarget = null;
        HandleHuntState(false);
    }


    void HandleHuntState(bool started)
    {
        isHunting = started;
        EnableMiscStuff();
    }
}
