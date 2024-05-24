using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedOutput : MonoBehaviour
{
    public Text text;
	public Rigidbody2D bullet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Скорость (м/с): \n" + bullet.velocity.magnitude;
    }
}
