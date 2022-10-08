using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField]
    private bool _focusPoint = false;

    private Vector3 _patrolPoint = Vector3.zero;

    private float _detectRadius = 1.0f;
  

 
    // Start is called before the first frame update
    void Start()
    {
        _patrolPoint = gameObject.transform.position;
    }
    public Vector3 PatrolPos
    {
        get 
        { 
            return _patrolPoint;
        }
  
    }

 

    public bool FocusThisPoint
    {
        get
        {
            return _focusPoint;
        }
        set
        {
            _focusPoint = value;
        }
    }

    // Update is called once per frame

    public bool CheckIfArrived(Vector3 position)
    {
        float distance = Vector3.Distance(position, _patrolPoint);

        if (distance < _detectRadius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
