using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    //include the UnityEngine.UI namespace
    [SerializeField]
    Image _healthBar = null;
    [SerializeField]
    Image _staminaBar = null;
    [SerializeField]
    Text _timeCountdown = null;
    [SerializeField]
    Text _timeTrade = null;
    [SerializeField]
    Text _currentHealth = null;
    [SerializeField]
    Text _startingHealth = null;



    private Health _playerHealth = null;
    private Stamina _playerStamina = null;
    // private ShootingBehaviour _playerShootingBehaviour = null;
    private BasicTimeCountdown _playerBasicTimeCountdown = null;

    private const int _dangerCountdown = 10;
    private float _timeTradeTimer = 0.0f;
    private  const float _lowOnHealth = 10.0f;
    private  bool _IsInRedTime = false;
    private bool _IsInBlackHealth = false;
    private const string ESCAPE = "Escape";
    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter2D player = FindObjectOfType<PlayerCharacter2D>();

        if (player != null)
        {
            _playerHealth = player.GetComponent<Health>();
            _playerStamina = player.GetComponent<Stamina>();
            _playerBasicTimeCountdown = player.GetComponent<BasicTimeCountdown>();
        }
        _timeTrade.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        SyncDate();
        EscapeKey();
    }


    void SyncDate()
    {
        //health
        if (_healthBar && _playerHealth && _currentHealth && _startingHealth)
        {
            HealthFeedback();
            _healthBar.transform.localScale = new Vector3(_playerHealth.HealthPercentage, 1.0f, 1.0f);
            _currentHealth.text = _playerHealth.CurrentHealth.ToString();
            _startingHealth.text = _playerHealth.StartingHealth.ToString();
        }
        //stamina
        if (_staminaBar && _playerStamina)
        {
            _staminaBar.transform.localScale = new Vector3(_playerStamina.StaminaPercentage, 1.0f, 1.0f);

        }
        //Time
        if (_timeCountdown && _playerBasicTimeCountdown)
        {
            TimeFeedback();
            _timeCountdown.text = _playerBasicTimeCountdown.CountdownTime.ToString();
        }
        //Time trade
        if (_timeTrade && _playerBasicTimeCountdown)
        {
            if (_playerBasicTimeCountdown.AmountTradeInASecond < 0)
            {
                _timeTradeTimer = 0.0f;
                _timeTrade.text =  _playerBasicTimeCountdown.AmountTradeInASecond.ToString();
            }
            else if(_playerBasicTimeCountdown.AmountTradeInASecond > 0)
            {
                _timeTradeTimer = 0.0f;
                _timeTrade.text = "+"+_playerBasicTimeCountdown.AmountTradeInASecond.ToString();
            }
            else if(_timeTradeTimer >= 2.0f)
            {
                _timeTrade.text = string.Empty;
            }
            _timeTradeTimer += Time.deltaTime;


        }
    }

    private void TimeFeedback()
    {
      
        if (!_IsInRedTime && _playerBasicTimeCountdown.CountdownTime <= _dangerCountdown && _playerBasicTimeCountdown.HasSecondPassed())
        {
            _IsInRedTime = true;
            _timeCountdown.color = Color.red;
        }
        else if(_playerBasicTimeCountdown.HasSecondPassed() && _IsInRedTime)
        {
            _IsInRedTime = false;
            _timeCountdown.color = Color.black;
        }

        
    }

    private void HealthFeedback()
    {

        if (!_IsInBlackHealth && _playerHealth.CurrentHealth <= _lowOnHealth && _playerBasicTimeCountdown.HasSecondPassed())
        {
            _IsInBlackHealth = true;
            _currentHealth.color = Color.black;
        }
        else if (_playerBasicTimeCountdown.HasSecondPassed() && _IsInBlackHealth)
        {
            _IsInBlackHealth = false;
            _currentHealth.color = Color.white;
        }


    }

    private void EscapeKey()
    {
        if (Input.GetAxis(ESCAPE) > 0.0f)
        {
            SceneManager.LoadScene(0);
        }
    }
}

