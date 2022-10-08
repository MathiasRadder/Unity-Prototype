using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate = null;
    [SerializeField] private int _clipSize = 50;
    [SerializeField] private float _fireRate = 25.0f;
    [SerializeField] private List<Transform> _fireSockets = new List<Transform>();
    [SerializeField] private AudioSource _fireSound;
    [SerializeField] private float _attackAnimationCooldown = 1.0f;
     private float _attackTimer = 0.0f;

    private bool _triggerPulled = false;
    private int _currentAmmo = 50;
    private float _fireTimer = 0.0f;
    private Animator _animator = null;

    const string PARAMETER_ATTACKING = "IsAttacking";
    const string STATE_IDLESWIPE = "IdleSwipe";

    public int CurrentAmmo
    {
        get
        {
            return _currentAmmo;
        }
    }


    private void Awake()
    {
        Animator tmpAn = GetComponentInChildren<Animator>();
        if (tmpAn)
        {
            _animator = tmpAn;
        }

        _currentAmmo = _clipSize;
    }
    private void Update()
    {
        //handle the countdown of the fire timer
        if (_fireTimer > 0.0f)
            _fireTimer -= Time.deltaTime;




        if (_animator && _triggerPulled && _attackTimer <= _attackAnimationCooldown)
        {
            _attackTimer += Time.deltaTime;
            return;
        }
        _attackTimer = 0.0f;

        if (_fireTimer <= 0.0f && _triggerPulled)
        {
            FireProjectile();
        

        }

        if (_animator)
        {
            _animator.SetBool(PARAMETER_ATTACKING, false);

        }
        //  _animator.GetCurrentAnimatorStateInfo(0).IsName(STATE_SWIPE);
        //the trigger will release by itself, 
        //if we still are firing, we will receive new fire input
        _triggerPulled = false;

    }

    private void FireProjectile()
    {
        //no ammo, we can't fire
        if (_currentAmmo <= 0)
            return;

        //no bullet to fire
        if (_bulletTemplate == null)
            return;

        //consume a bullet
        --_currentAmmo;

        for (int i = 0; i < _fireSockets.Count; i++)
        {
            Instantiate(_bulletTemplate,
                _fireSockets[i].position, _fireSockets[i].rotation);
        }

        //set the time so we respect the firerate
        _fireTimer += 1.0f / _fireRate;
        if (_fireSound)
        {
            _fireSound.Play();
        }
    }

    public void Fire()
    {
        _triggerPulled = true;

        if (_animator)
        {
            _animator.SetBool(PARAMETER_ATTACKING, true);
        }
    }
    public void Reload()
    {
        _currentAmmo = _clipSize;
    }
}
