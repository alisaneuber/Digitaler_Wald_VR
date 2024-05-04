using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Deer_Movement : MonoBehaviour
{
    // Animation States
    public Animator deerAnimator;
    private const string DEER_STAND = "deer|stand";
    private const string DEER_WALK = "deer|walkingState";
    private const string DEER_REACT = "deer|react";
    string currentState;
    public bool isEnabled = false;


    [SerializeField]
    Transform[] targets;
    NavMeshAgent _navMeshAgent;
    Vector3 targetVector;
    private int currentTargetIndex = 0;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if(_navMeshAgent == null) {
            Debug.LogError("Nav mesh agent not attached to " + gameObject.name);
        }
        else {
            SetNextDestination();
        }

        if (deerAnimator == null)
        {
            deerAnimator = GetComponent<Animator>();
            if (deerAnimator == null)
            {
                Debug.LogError("Animator component not found!");
                enabled = false;
                return;
            }
        }
    }

    private void SetNextDestination()
    {
        if(targets != null && targets.Length > 0) {
            targetVector = targets[currentTargetIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    private void ChangeAnimationState(string state)
    {
        if(state == currentState){
            return;
        }
        deerAnimator.Play(state);
        currentState = state;
    }

    private bool isMoving(){
        return (_navMeshAgent.velocity.magnitude > 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving() && !isReacting())
        {
            ChangeAnimationState(DEER_WALK);
        }
        else if (!isMoving() && !isReacting())
        {
            ChangeAnimationState(DEER_STAND);
        }

        if (_navMeshAgent.remainingDistance <= 0.2f)
        {
            CheckDestinationReached();
        }

        if (isReacting())
        {
            _navMeshAgent.isStopped = true;
        }
    }
    
    public void ChangeTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        SetNextDestination();
    }

    public IEnumerator ReactCoroutine()
    {
        ChangeAnimationState(DEER_REACT);
        yield return new WaitForSeconds(0.833f);
    }

    IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(0.1f);
        if (_navMeshAgent.remainingDistance <= 0.2f)
        {
            ChangeTarget();
        }
    }

    void CheckDestinationReached()
    {
        StartCoroutine(CheckDistance());
    }
    
    public void react()
    {
        StartCoroutine(ReactCoroutine());
    }

    private bool isReacting()
    {
        return deerAnimator.GetCurrentAnimatorStateInfo(0).IsName("deer|react");
    }

    public void enableAI()
    {
        _navMeshAgent.isStopped = false;
        ChangeTarget();
    }
}

