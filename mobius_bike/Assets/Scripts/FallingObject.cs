using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
	public float lifetime = 20.0f;
	public float speed = 0.1f;
	private float initTime;
    // Start is called before the first frame update
    void Start()
    {
		initTime = Time.time;
		Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {	
		
        transform.position += Vector3.down*speed;	


		//if (Time.time > initTime + lifetime)
        //{
        //    Destroy(gameObject);
        //}		
		
    }
}
