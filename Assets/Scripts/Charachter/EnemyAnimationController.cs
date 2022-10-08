using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Vector3 _previousPosition;
    private Animator _animator = null;
    private bool _isMoving = false;
    private void Awake()
    {
        _previousPosition = transform.root.position;

        _animator = transform.GetComponent<Animator>();
        _animator.Play("Move");
    }

    private void Update()
    {
        HandleMovementAnimation();
    }

    const string IS_MOVING_PARAMETER = "IsMoving";
   // const string IS_MOVING_PARAMETER = "Moving";
    void HandleMovementAnimation()
    {
        if (_animator == null)
        {
            return;
        }
    

       
        // _animator.SetBool(IS_MOVING_PARAMETER,(transform.root.position - _previousPosition).sqrMagnitude > 0.0001f );

        //_previousPosition = transform.root.position;
      //  _animator.Play("Move");
    }
}
