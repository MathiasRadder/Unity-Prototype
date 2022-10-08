using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovementBehaviour : MovementBehaviour
{
    //include the namespace UnityEngine.AI


    public enum ActionState
    {
        patrol,
        chace,
        none
    }



    [SerializeField]
    private ActionState _actionState = ActionState.none;

    [SerializeField]
    private PatrolPoint _patrolStart = null;

    [SerializeField]
    private PatrolPoint _patrolEnd = null;

    
    private NavMeshAgent _navMeshAgent = null;

    private PerceptionBehaviour _perceptionBeh;

    private Vector3 _previousTargetPosition = Vector3.zero;

    const string FRIENDLY_LAYER = "Friendly";


    public ActionState CurrentActionState
    {
        get
        {
            return _actionState;
        }

    }

    protected override void Awake()
    {
        base.Awake();

        PerceptionBehaviour perBehTmp = GetComponentInChildren<PerceptionBehaviour>(); ;
        if (perBehTmp)
        {
            _perceptionBeh = perBehTmp;
        }


        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;

        _previousTargetPosition = transform.position;

        if (_patrolStart == null || _patrolEnd == null)
        {
            _actionState = ActionState.none;

        }
        else
        {
            _navMeshAgent.SetDestination(_patrolStart.PatrolPos);
            _previousTargetPosition = _patrolStart.PatrolPos;
            _desiredLookatPoint = _patrolEnd.PatrolPos;
            _patrolStart.FocusThisPoint = true;
            _patrolEnd.FocusThisPoint = false;
            _navMeshAgent.isStopped = false;
  
        }
       

    }

    const float MOVEMENT_EPSILON = .25f;
    protected override void HandleMovement()
    {
        if (_target == null)
        {
            _navMeshAgent.isStopped = true;
            return;
        }

        HandlePerception();

        switch (_actionState)
        {
            case ActionState.patrol:
                HandlePatrolMovement();
                break;
            case ActionState.chace:
                HandleChaceMovement();
                break;
            case ActionState.none:
                break;
            default:
                break;
        }
    

      
      


        transform.LookAt(_desiredLookatPoint, Vector3.up);
    }
    void HandlePerception()
    {

        if (_perceptionBeh.TargetInTrigger)
        {
            _actionState = ActionState.chace;
        }


      

        // float dotRIght = Vector3.Dot(transform.right, _target.transform.right);

        //if (dotRIght > 0.0f && dotRIght > 0.00001f && dotRIght <-)
        //{

        //    // _desiredLookatPoint = _target.transform.position;
        //    //add raycast or spherecasrt or something to cehck

        //       Debug.Log("it hits right:  " + dotRIght.ToString());

        //    return;

        //}


    }

    public void HandlePatrolMovement()
    {


     

        if (_patrolStart.FocusThisPoint && _patrolStart.CheckIfArrived(transform.position))
        {
            _navMeshAgent.SetDestination(_patrolEnd.PatrolPos);
            _previousTargetPosition = _patrolEnd.PatrolPos;
            _patrolStart.FocusThisPoint = false;
            _patrolEnd.FocusThisPoint = true;
            _navMeshAgent.isStopped = false;

        }
        else if(_patrolEnd.FocusThisPoint && _patrolEnd.CheckIfArrived(transform.position))
        {
            _navMeshAgent.SetDestination(_patrolStart.PatrolPos);

            _previousTargetPosition = _patrolStart.PatrolPos;
            _patrolEnd.FocusThisPoint = false;
            _patrolStart.FocusThisPoint = true;
            _navMeshAgent.isStopped = false;
        }


        if (_patrolStart.FocusThisPoint)
        {
            _desiredLookatPoint = _patrolStart.PatrolPos;
       
        }
        else
        {
            _desiredLookatPoint = _patrolEnd.PatrolPos;
        }


    }

    public void HandleChaceMovement()
    {
       
        //should the target move we should recalculate our path
        if ((_target.transform.position - _previousTargetPosition).sqrMagnitude
            > MOVEMENT_EPSILON)
        {
            _desiredLookatPoint = _target.transform.position;
            _navMeshAgent.SetDestination(_target.transform.position);
            _navMeshAgent.isStopped = false;
            _previousTargetPosition = _target.transform.position;

        }
    }

 
}

