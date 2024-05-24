using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCurrency : MonoBehaviour
{
	public CoilForce[] coilForces;
	public Slider slider;
	public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		text.text = "Сила тока (A): "+slider.value;
        for (int i = 0; i < coilForces.Length; i++) 
		{
			coilForces[i].currency = slider.value;
		}
    }
}
