using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private int _startingStamina = 10;
    [SerializeField]
    private float _staminaRegenCooldown = 0.5f;

    private int _currentStamina = 0;
    private float _staminaRegenTimer= 0.0f;


    //in the Health Script
    public float StaminaPercentage
    {
        get
        {
            return ((float)_currentStamina) / _startingStamina;
        }
    }


    public int CurrentStamina
    {
        get
        {
            return _currentStamina;
        }

    }

    void Awake()
    {
        _currentStamina = _startingStamina;
    }




    public void StaminaCost(int amount)
    {
        _currentStamina -= amount;
        if (_currentStamina < 0)
        {
            _currentStamina = 0;
        }
    }

    public void RegenStamina()
    {
        if (_staminaRegenTimer >= _staminaRegenCooldown && _currentStamina < _startingStamina)
        {
            _currentStamina++;
            _staminaRegenTimer = 0.0f;
        }
        else
        {
            _staminaRegenTimer += Time.deltaTime;
        }
    }
}
