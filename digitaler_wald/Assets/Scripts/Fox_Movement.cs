using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fox_Movement : MonoBehaviour
{
    // Animation States
    public Animator foxAnimator;
    private const string FOX_STAND = "Fox|stand";
    private const string FOX_WALK = "Fox|walk";
    private const string FOX_REACT = "Fox|react";
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

        if (foxAnimator == null)
        {
            foxAnimator = GetComponent<Animator>();
            if (foxAnimator == null)
            {
                Debug.LogError("Animator component not found!");
                enabled = false;
                return;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving() && !isReacting())
        {
            ChangeAnimationState(FOX_WALK);
        }
        else if (!isMoving() && !isReacting())
        {
            ChangeAnimationState(FOX_STAND);
        }

        if (_navMeshAgent.remainingDistance <= 0.2f)
        {
            CheckDestinationReached();
        }

        if (isReacting())
        {
            _navMeshAgent.isStopped = true;
        }
        RotateObject();
    }

    void RotateObject()
    {
        if (_navMeshAgent.velocity.magnitude > 0.1f)
        {
            Vector3 direction = _navMeshAgent.velocity.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion additionalRotation = Quaternion.Euler(0, -92, 0); // Additional rotation on the z-axis
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation * additionalRotation, Time.deltaTime * 10f); // Adjust rotation speed here
        }
    }

    private void SetNextDestination()
    {
        if(targets != null && targets.Length > 0) {
            targetVector = targets[currentTargetIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            Debug.Log(_navMeshAgent.velocity.normalized);
        }
    }

    private void ChangeAnimationState(string state)
    {
        if(state == currentState){
            return;
        }
        foxAnimator.Play(state);
        currentState = state;
    }

    private bool isMoving(){
        return (_navMeshAgent.velocity.magnitude > 0.2f);
    }
    
    public void ChangeTarget()
    {
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        SetNextDestination();
    }

    public IEnumerator ReactCoroutine()
    {
        ChangeAnimationState(FOX_REACT);
        yield return new WaitForSeconds(0.833f);
    }

    IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(1f);
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
        return foxAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fox|react");
    }

    public void enableAI()
    {
        _navMeshAgent.isStopped = false;
        ChangeTarget();
        ChangeTarget();
    }
}
