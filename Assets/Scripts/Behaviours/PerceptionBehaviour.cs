using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string TARGET_TAG = "Friendly";

    private bool _isInTrigger = false;




    public bool TargetInTrigger
    {
        get
        {
            return _isInTrigger;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //make sure we only hit friendly or enemies
        if (other.tag == TARGET_TAG)
        {
            _isInTrigger = true;
        }




    }
}
