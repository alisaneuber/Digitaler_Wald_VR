using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf_Movement : MonoBehaviour
{
    public Animator wolfAnimator;
    private const string WOLF_STAND = "Wolf|stand";
    private const string WOLF_WALK = "Wolf|walk";
    private const string WOLF_REACT = "Wolf|react";
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

        if (wolfAnimator == null)
        {
            wolfAnimator = GetComponent<Animator>();
            if (wolfAnimator == null)
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
        wolfAnimator.Play(state);
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
            ChangeAnimationState(WOLF_WALK);
        }
        else if (!isMoving() && !isReacting())
        {
            ChangeAnimationState(WOLF_STAND);
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
        ChangeAnimationState(WOLF_REACT);
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
        return wolfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Wolf|react");
    }

    public void enableAI()
    {
        _navMeshAgent.isStopped = false;
        ChangeTarget();
    }
}
