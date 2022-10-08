using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter2D : BasicCharacter2D
{


    [SerializeField]
    private float _dashCooldownAmount = 1.0f;


    [SerializeField]
    private float _sprintCostCooldown = 0.5f;

    private float _dashTimer = 0.0f;
    private float _sprintingTime = 0.0f;

    const string MOVEMENT_HORIZONTAL = "MovementHorizontal";
    const string JUMP = "Jump";
    const string DASH = "Dash";
    const string SPRINT = "Sprint";
    const string PRIMARY_FIRE = "PrimaryFire";

    private bool _islookingRight = true;
    private bool _hasDashMidAir = false;
    private BasicTimeCountdown _basicTimeCountdown = null;
    private Health _health = null;
    private Stamina _Stamina = null;

    [SerializeField]
    GameObject _attackVFXTemplate = null;




   


        // private float _

        // Start is called before the first frame update
        void Start()
    {

    }



    protected override void Awake()
    {
        base.Awake();

        //_animator = transform.GetComponentFromChildren<Animator>();

        _basicTimeCountdown = GetComponent<BasicTimeCountdown>();
        _Stamina = GetComponent<Stamina>();
        _health = GetComponent<Health>();

    }
    // Update is called once per frame
    private void Update()
    {
        HandleMovementInput();
        HandleFireInput();
        HandleSprint();
        HandleJump();
        HandleDash();
        HandleTimerCountDown();
        HandleStamina();


    }
    private void HandleFireInput()
    {

        if (_shootingBehaviour == null) return;

        //fire
        if (Input.GetAxis(PRIMARY_FIRE) > 0.0f)
            _shootingBehaviour.PrimaryFire();
    }
    void HandleMovementInput()
    {
        if (_movementBehaviour2D == null)
        {
            return;
        }

        //movement
        float horizontalMovement = Input.GetAxis(MOVEMENT_HORIZONTAL);

        HandleLookDirection(horizontalMovement);

        Vector3 movement = horizontalMovement * Vector3.right /*+ verticalMovement * Vector3.forward*/;

        _movementBehaviour2D.DesiredMovementDirection = movement;


    }

  private void HandleLookDirection(float horizontalMovement)
    {

        float angle = 180.0f;
        if (horizontalMovement > 0.0f && !_islookingRight)
        {
            transform.Rotate(Vector3.up, angle);
            _islookingRight = true;
        }
        else if (horizontalMovement < 0.0f && _islookingRight)
        {
            transform.Rotate(Vector3.up, angle);
            _islookingRight = false;
        }

   
    }

    const int _minTimeDamge = 4;
    void HandleTimerCountDown()
    {
        if (_basicTimeCountdown.tookTime)
        {
            if (_attackVFXTemplate)
            {
                Vector3 tmp = transform.position;
                tmp.y += transform.localScale.y;
                Instantiate(_attackVFXTemplate, tmp, transform.rotation);
                _basicTimeCountdown.tookTime = false;
            }
        }

        if (_basicTimeCountdown.CountdownTime < 0 && _basicTimeCountdown.HasSecondPassed())
        {
            if (((int)_health.CurrentHealth) <= _minTimeDamge)
            {
                _health.Damage(1);

            }
            else
            {
              _health.Damage(((int)_health.CurrentHealth) / 5); 

            }

            if (_attackVFXTemplate)
            {
                Vector3 tmp = transform.position;
                tmp.y += transform.localScale.y;
                Instantiate(_attackVFXTemplate, tmp, transform.rotation);
            }


        }
    }



    private void HandleJump()
    {
        if (Input.GetAxis(JUMP) > 0.0f && _movementBehaviour2D.OnGround)
        {


            _movementBehaviour2D.Jump();
            _movementBehaviour2D.OnGround = false;

        }
    }

    private void HandleDash()
    {
        _dashTimer += Time.deltaTime;
        if (!_movementBehaviour2D.OnGround && _hasDashMidAir)
        {
            return;
        }
        else if(_movementBehaviour2D.OnGround)
        {
            _hasDashMidAir = false;
        }

        if (_Stamina == null)
        {
            return;
        }

        if ((Input.GetAxis(DASH) > 0.0f || Input.GetMouseButton(1)) && _dashTimer >= _dashCooldownAmount && _Stamina.CurrentStamina > 0)
        {
            _hasDashMidAir = !_movementBehaviour2D.OnGround;

            float totalTimeSpeed = _basicTimeCountdown.CountdownTime / (float)(_basicTimeCountdown.StartingTime);
            if (totalTimeSpeed < 0.5f)
            {
                totalTimeSpeed = 0.5f;
            }
         
   
            _movementBehaviour2D.Dash(totalTimeSpeed);


            int dashCost = 2;
            _dashTimer = 0.0f;
            if (_Stamina.CurrentStamina > 0)
            {

                _Stamina.StaminaCost(dashCost);

            }
            else
            {
                _basicTimeCountdown.TimeTrade(-dashCost);
            }

        }

    }

    void HandleStamina()
    {
        if (_Stamina == null)
        {
            return;
        }

        if (_dashTimer >= _dashCooldownAmount && _movementBehaviour2D.OnGround && _sprintingTime  < 0.001f)
        {
            _Stamina.RegenStamina();
        }
    }

    private void HandleSprint()
    {
        if (!_movementBehaviour2D.OnGround)
        {
            return;
        }

        bool isSprinting = Input.GetAxis(SPRINT) > 0.0f && _Stamina.CurrentStamina > 0;
        _movementBehaviour2D.Sprint(isSprinting);

        if (_Stamina == null)
        {
            return;
        }
        int sprintCost = 1;
        if (isSprinting)
        {
            if (_Stamina.CurrentStamina > 0 && _sprintingTime > _sprintCostCooldown)
            {
                _Stamina.StaminaCost(sprintCost);
                _sprintingTime = 0.0f;

            }
            else if(_sprintingTime > _sprintCostCooldown)
            {
                _basicTimeCountdown.TimeTrade(-sprintCost);
                _sprintingTime = 0.0f;
            }


            _sprintingTime += Time.deltaTime;
        }
        else
        {
            _sprintingTime = 0.0f;
        }
    }
}

