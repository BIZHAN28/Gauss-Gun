using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneSolenoid : MonoBehaviour
{
    public GameObject solenoid;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space"))
        {
            solenoid.SetActive(true);
        }
        else {
            solenoid.SetActive(false);
        }
    }
}
