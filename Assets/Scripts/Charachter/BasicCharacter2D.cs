using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter2D : MonoBehaviour
{
    protected MovementBehaviour2D _movementBehaviour2D;
    protected ShootingBehaviour _shootingBehaviour;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        _movementBehaviour2D = GetComponent<MovementBehaviour2D>();
        _shootingBehaviour = GetComponent<ShootingBehaviour>();
    }
}

