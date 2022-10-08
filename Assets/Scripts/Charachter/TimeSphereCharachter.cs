using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSphereCharachter : MonoBehaviour
{
    [SerializeField]
    private int _timeReward = 1;

    string TARGET_TAG = "Friendly";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TARGET_TAG)
        {
            BasicTimeCountdown tmpCompT = other.GetComponent<BasicTimeCountdown>();
            if (tmpCompT)
            {
                tmpCompT.TimeTrade(_timeReward);
                Kill();
            }


        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
