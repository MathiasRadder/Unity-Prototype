using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKamikazeCharacter2D : BasicCharacter
{
    private GameObject _playerTarget = null;
    [SerializeField]
    private float _attackRange = 2.0f;
    [SerializeField]
    GameObject _attackVFXTemplate = null;
    private bool _hasAttacked = false;

    private void Start()
    {
        //expensive method, use with caution
        PlayerCharacter2D player = FindObjectOfType<PlayerCharacter2D>();

        if (player) _playerTarget = player.gameObject;

        _movementBehaviour.Target = _playerTarget;
        _movementBehaviour.DesiredLookatPoint = _playerTarget.transform.position;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttacking();
    }

    void HandleMovement()
    {
        if (_movementBehaviour == null || _playerTarget == null)
            return;

      
       // _movementBehaviour.Target = _playerTarget;
      //  _movementBehaviour.DesiredLookatPoint = _playerTarget.transform.position;

    }

    void HandleAttacking()
    {
        if (_hasAttacked) return;

        if (_shootingBehaviour == null) return;

        if (_playerTarget == null) return;

        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude
            < _attackRange * _attackRange)
        {
            _shootingBehaviour.PrimaryFire();
            _hasAttacked = true;

            if (_attackVFXTemplate)
            {
                Instantiate(_attackVFXTemplate, transform.position, transform.rotation);
            }

            //this is a kamikaze enemy, 
            //when it fires, it should destroy itself

            Invoke(KILL_METHODNAME, 0.2f);
        }
    }

    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        Destroy(gameObject);
    }



  
}

