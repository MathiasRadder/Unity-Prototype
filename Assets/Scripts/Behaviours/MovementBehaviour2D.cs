using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementBehaviour2D : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 1.0f;

    [SerializeField]
    protected float _sprintSpeed = 2.0f;

    [SerializeField]
    private float _jumpForce = 300.0f;

    private float _startingSpeed = 1.0f;

    [SerializeField]
    private AnimationCurve _animationCurve = new AnimationCurve();
    [SerializeField]
    private float _dashDuration = 0.3f;

    [SerializeField]
    private float _dashTimeForce = 1.2f;

    private float _dashTimer = 0.0f;
    private float _totalForce = 0.0f;
    private float _ForceRelativeToTime = 0.0f;

    protected Rigidbody _rigidBody;

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    const string GROUND_LAYER = "Ground";


    private bool _isOnGround = false;
    private bool _mouseTotheRight = true;
    private Plane _cursorMovementPlane;

    public Vector3 DesiredMovementDirection
    {
        get { return _desiredMovementDirection; }
        set { _desiredMovementDirection = value; }
    }

    protected Vector3 _desiredLookatPoint = Vector3.zero;
    public Vector3 DesiredLookatPoint
    {
        get { return _desiredLookatPoint; }
        set { _desiredLookatPoint = value; }
    }


    public bool IsMouseAtTheRight
    {
        get { return _mouseTotheRight; }
    }

    protected GameObject _target;
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public bool OnGround
    {
        get { return _isOnGround; }
        set { _isOnGround = value; }

    }

    protected virtual void Awake()
    {
        _dashTimer = _dashDuration;
        _rigidBody = GetComponent<Rigidbody>();
        _startingSpeed = _movementSpeed;
        _cursorMovementPlane = new Plane(Vector3.up, transform.position);
    }

    protected virtual void Update()
    {
        // HandleRotation();
        CalculateLookDirection();


    }
    protected virtual void FixedUpdate()
    {
        HandleMovement();
        CheckOnGround();
        CheckIsAgainstWall();
        Dashing();

    }

    protected virtual void HandleMovement()
    {


        // ressetting pos z
        //Vector3 pos = _rigidBody.position;
        //pos.z = 0.0f;
        //_rigidBody.position = pos;

        Vector3 movement = _desiredMovementDirection.normalized;

        movement *= _movementSpeed;


        movement.y = _rigidBody.velocity.y;
        _rigidBody.velocity = movement;



    }

    protected virtual void HandleRotation()
    {
        transform.LookAt(_desiredLookatPoint, Vector3.up);
    }


    private void CheckOnGround()
    {

        RaycastHit hit;
        _isOnGround = Physics.SphereCast(transform.position + Vector3.up, 0.3f, -Vector3.up, out hit, 1.0f, LayerMask.GetMask(GROUND_LAYER));
        //  Debug.Log(hit.collider.gameObject);


    }

    private void CheckIsAgainstWall()
    {
        // RaycastHit hit;
        Vector3 reverseVec;
        reverseVec = _rigidBody.velocity;
        reverseVec.x = 0.0f;
        Vector3 point1 = transform.position;//make this in start or awake dont do this local
        point1.y -= ((transform.localScale.y / 2) - 0.5f);
        Vector3 point2 = transform.position;
        point2.y += (transform.localScale.y / 2) + 1.0f;
        const float size = 0.3f;

        if (_rigidBody.velocity.x > 0.0f)
        {
            if (Physics.CapsuleCast(point1, point2, size, Vector3.right, size, LayerMask.GetMask(GROUND_LAYER)))
            {
                _rigidBody.velocity = reverseVec;
            }


        }
        else if (_rigidBody.velocity.x < 0.0f)
        {
            if (Physics.CapsuleCast(point1, point2, size, Vector3.left, size, LayerMask.GetMask(GROUND_LAYER)))
            {
                _rigidBody.velocity = reverseVec;
            }
        }

    }

    public void Jump()
    {

        _rigidBody.AddForce(transform.up * _jumpForce);
    }


    private void Dashing()
    {


        //calculate so that dashduration becomes one
        _totalForce = _animationCurve.Evaluate((_dashTimer / _dashDuration));//_animationCurve is the force
        _totalForce *= _ForceRelativeToTime;
        _rigidBody.AddForce(transform.right * _totalForce, ForceMode.Impulse);

        _dashTimer += Time.deltaTime;



    }
    public void Dash()
    {
        if (_dashTimer >= _dashDuration)
        {
            _dashTimer = 0.0f;
        }

    }

    public void Dash(float forceRelTime)
    {

   
         _ForceRelativeToTime = forceRelTime* _dashTimeForce;
        
        if (_dashTimer >= _dashDuration)
        {
            _dashTimer = 0.0f;
        }

    }
    public void Sprint(bool IsRunning)
    {

        if (IsRunning && _isOnGround)
        {
            _movementSpeed = _sprintSpeed;
        }
        else
        {
            _movementSpeed = _startingSpeed;
        }
    }

    private void CalculateLookDirection()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 positionOfMouseInWorld = transform.position;

        if (Physics.Raycast(mouseRay, out RaycastHit HitInfo, 100000.0f, LayerMask.GetMask(GROUND_LAYER)))
        {
            positionOfMouseInWorld = HitInfo.point;

        }
        else
        {
            _cursorMovementPlane.Raycast(mouseRay, out float distance);
            positionOfMouseInWorld = mouseRay.GetPoint(distance);
        }

        _mouseTotheRight = positionOfMouseInWorld.x >= _rigidBody.position.x;


    }



}