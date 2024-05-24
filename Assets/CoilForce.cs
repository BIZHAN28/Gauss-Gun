using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoilForce : MonoBehaviour
{
	public Transform bullet;
	public float wireLength = 1f;
	public float currency = 1f;
	public float coilTurns = 10f;
	public float distance;
	
    void Start()
    {
        
    }
    void Update()
    {
		distance = transform.position.x - bullet.position.x;
        float F = 1.26f * MathF.Pow(10f, -6f) * coilTurns * MathF.Pow(currency, 2f) * wireLength / distance;
		GetComponent<PointEffector2D>().forceMagnitude = -F;
    }
}
