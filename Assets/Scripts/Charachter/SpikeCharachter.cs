using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCharachter : BasicCharacter
{

    private GameObject _playerTarget = null;

    [SerializeField]
    private float _upMovementSpeed = 1.0f;

    [SerializeField]
    private float _downMovementSpeed = 1.0f;


    [SerializeField]
    private float _startingTrapTime = 0.01f;

    [SerializeField]
    private float _waitingDownTrapTime = 0.0f;

    [SerializeField]
    private float _waitingUpTrapTime = 0.0f;


    private enum TrapStates
    {
        trapIsDown,
        trapIsUp,
        goingUp,
        goingDown,
        waitingForStart
    }

    private float _timer = 0.0f;
    private TrapStates _trapState = TrapStates.waitingForStart;


    Vector3 _movementVector = Vector3.zero;
    float _maxHeight = 0.0f;
    string TARGET_TAG = "Friendly";

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter2D player = FindObjectOfType<PlayerCharacter2D>();

        if (player) _playerTarget = player.gameObject;
        _movementVector = transform.position;
        // _maxHeight = transform.position.y + 1.0f;
        _maxHeight = transform.position.y + transform.localScale.y;

        if (_waitingDownTrapTime < 0.1f)
        {
            _waitingDownTrapTime = 0.1f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (_trapState)
        {
            case TrapStates.waitingForStart:
                WaitingToGo(_startingTrapTime, TrapStates.goingUp);
                break;
            case TrapStates.goingUp:
                TrapGoingUpMovement();
                break;
            case TrapStates.goingDown:
                TrapGoingDownMovement();
                break;
            case TrapStates.trapIsUp:
                WaitingToGo(_waitingUpTrapTime, TrapStates.goingDown);
                break;
            case TrapStates.trapIsDown:
                WaitingToGo(_waitingDownTrapTime, TrapStates.goingUp);
                break;
            default:
                break;
        }

       

       // Movement();
    }


   
    private void WaitingToGo(float timeCooldown, TrapStates state)
    {
        _timer += Time.deltaTime;
        if (_timer >= timeCooldown)
        {
            _timer = 0.0f;
            _trapState = state;
        }
      
    }

    private void TrapGoingUpMovement()
    {
        _movementVector = transform.position;
        if (_movementVector.y < _maxHeight)
        {
            _movementVector.y += _upMovementSpeed * Time.deltaTime;
            transform.position = _movementVector;
        }
        else
        {
            _trapState = TrapStates.trapIsUp;
        }
    }

    private void TrapGoingDownMovement()
    {
        _movementVector = transform.position;
        if (_movementVector.y > _maxHeight - transform.localScale.y)
        {
            _movementVector.y -= _downMovementSpeed * Time.deltaTime;
            transform.position = _movementVector;
        }
        else
        {
            _trapState = TrapStates.trapIsDown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TARGET_TAG != other.tag)
        {
            return;
        }
        if (_shootingBehaviour == null) return;

        if (_playerTarget == null) return;
        _shootingBehaviour.PrimaryFire();
    }
}
