using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZoneCharachter : MonoBehaviour
{
    [SerializeField]
    private bool _damageTime = false;

    [SerializeField]
    private float _cooldownDamage = 1.0f;

    [SerializeField]
    private int _damage = 5;

    private float _timer = 0.0f;
    private bool _isInZone = false;

    private Health _playerHealth = null;
    private BasicTimeCountdown _playerTime = null;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter2D player = FindObjectOfType<PlayerCharacter2D>();
        if (player != null)
        {
            _playerHealth = player.GetComponent<Health>();
            _playerTime = player.GetComponent<BasicTimeCountdown>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        HandlePLayerInZone();
        _timer += Time.deltaTime;
    }

    private void HandlePLayerInZone()
    {
        if (_isInZone && _timer >= _cooldownDamage)
        {
            DamagePlayer();
            _timer = 0.0f;
        }
      
    }
    private void DamagePlayer()
    {
        if (_damageTime)
        {

            if (_playerTime != null)
            {
                _playerTime.TimeTrade(-_damage);

            }
        }
        else
        {

            if (_playerHealth != null)
            {
                _playerHealth.Damage(_damage);
            }
        }
    }
    const string FRIENDLY_TAG = "Friendly";
    const string ENEMY_TAG = "Enemy";


    void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly or enemies
        if (other.tag != FRIENDLY_TAG && other.tag != ENEMY_TAG)
            return;

        //only hit the opposing team
        if (other.tag == tag)
            return;

        _isInZone = true;
        DamagePlayer();


    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag != FRIENDLY_TAG && other.tag != ENEMY_TAG)
            return;

        //only hit the opposing team
        if (other.tag == tag)
            return;


        _isInZone = false;
    }
}
