using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 10;

    private int _currentHealth = 0;

    [SerializeField]
    private Color _flickerColor = Color.white;
    [SerializeField]
    private float _flickerDuration = 0.1f;

    private Color _startColor;
    private Material _attachedMaterial;
    const string COLOR_PARAMETER = "_Color";

    //in the Health Script
    public float HealthPercentage
    {
        get
        {
            return ((float)_currentHealth) / _startHealth;
        }
    }
    public float StartingHealth
    {
        get
        {
            return _startHealth;
        }

    }

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }

    }

    void Awake()
    {
        _currentHealth = _startHealth;
    }

    private void Start()
    {
        Renderer renderer = transform.GetComponentInChildren<Renderer>();
        if (renderer)
        {
            _attachedMaterial = renderer.material;

            if (_attachedMaterial)
            {
                _startColor = _attachedMaterial.GetColor(COLOR_PARAMETER);
            }
        }
    }

 

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        if (_attachedMaterial)
        {
            _attachedMaterial.SetColor(COLOR_PARAMETER, _flickerColor);
            Invoke(RESET_COLOR_METHOD, _flickerDuration);
        }

        if (_currentHealth <= 0)
            Kill();
    }

    const string RESET_COLOR_METHOD = "ResetColor";

    void ResetColor()
    {
        if (!_attachedMaterial)
        {
            return;
        }
        _attachedMaterial.SetColor(COLOR_PARAMETER, _startColor);
    }

    void Kill()
    {
        Destroy(gameObject);
    
    }

    private void OnDestroy()
    {
        if (_attachedMaterial == null)
        {
            return;
        }
        Destroy(_attachedMaterial);
    }
}


