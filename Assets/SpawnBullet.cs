using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public Vector3 trans;
	public Vector3 rotation;
	

    public void BringBack(GameObject bullet) 
    { 
		bullet.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        bullet.transform.position = trans;
		bullet.transform.Rotate(Vector3.right, Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
