using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTimeCountdown : MonoBehaviour
{
    [SerializeField]
    private int _startingTimeAmount = 20;

    private float _timeAmount = 0.0f;
    private int _secondChecker = 0;
    private int _amountTradeInASecond = 0;
    private bool _hasSecondPassed = false;
    private bool _enemyTookTime = false;

 
public int CountdownTime
    {
        get
        {
            return (int)(_timeAmount);
        }

    }
    public int AmountTradeInASecond
    {
        get
        {
            return _amountTradeInASecond;
        }

    }

    public int StartingTime
    {
        get
        {
            return _startingTimeAmount;
        }

    }

    public bool tookTime
    {
        get
        {
            return _enemyTookTime;
        }
        set
        {
            _enemyTookTime = value;
        }

    }


    public bool HasSecondPassed()
    {
        return _hasSecondPassed;
    }
    // Start is called before the first frame update
    void Start()
    {
        _timeAmount = _startingTimeAmount;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimeCountdown();
    }

    private void HandleTimeCountdown()
    {
        _timeAmount -= Time.deltaTime;
        if (_secondChecker != (int)(_timeAmount))
        {
            _secondChecker = (int)(_timeAmount);
            _hasSecondPassed = true;
            _amountTradeInASecond = 0;
        }
        else
        {
            _hasSecondPassed = false;
        }

    }


    public void TimeTrade(int timeAmount)
    {
               _timeAmount += timeAmount;
        _amountTradeInASecond += timeAmount;

            _enemyTookTime = true;

    }
}
