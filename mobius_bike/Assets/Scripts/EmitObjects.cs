using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitObjects : MonoBehaviour
{
    public GameObject obj;
	//public GameObject targetObject;
	private float nextSpawnTime = 0.0f;
	private float nextEvilnessTime = 0.0f;
    public float emitPeriod = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time > nextSpawnTime)
		{
				nextSpawnTime = Time.time + emitPeriod;

				GameObject fallingObject = Instantiate(obj, transform.position, Quaternion.identity) as GameObject;
				//fallingObject.AddComponent<FallingObject>();
				//lightningObject.GetComponent<ThrowableObject>().targetObject = targetObject;
				
		}
    }
}
