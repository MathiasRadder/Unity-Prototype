using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySphereCharachter : MonoBehaviour
{
    private GameObject _playerTarget = null;
    private SphereCollider _sphereCollider;
    private LevelCompleted _levelCompleted;
    private const float _detectRange = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter2D player = FindObjectOfType<PlayerCharacter2D>();

        if (player) _playerTarget = player.gameObject;
        _sphereCollider = GetComponent<SphereCollider>();
        _levelCompleted = GetComponent<LevelCompleted>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePLayerCollision();
    }
    private void HandlePLayerCollision()
    {

        if (_playerTarget == null) return;

        
        //if we are in range of the player, fire our weapon, 
        //use sqr magnitude when comparing ranges as it is more efficient
        if ((transform.position - _playerTarget.transform.position).sqrMagnitude
            < _sphereCollider.radius+ _detectRange)
        {
            _levelCompleted.TriggerCompleetLevel();
           
        }
    }


}
