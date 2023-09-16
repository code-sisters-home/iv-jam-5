using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public GameObject lightningPrefab;
	public GameObject targetObject;
	private float nextSpawnTime = 0.0f;
	private float nextEvilnessTime = 0.0f;
    public float lightningPeriod = 2f;
	public float evilnessPeriod = 20f;
	public float evilnessTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Time.time > nextEvilnessTime && Time.time < nextEvilnessTime + evilnessTime)
		{
			float evilness = 1 - Mathf.Abs((Time.time - nextEvilnessTime)/evilnessTime - 0.5f)*2.0f;
			float k = Mathf.Lerp(1.0f, 0.3f, evilness);
			GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(k, k, k, 1));
			
			if (Time.time > nextSpawnTime)
			{
				nextSpawnTime = Time.time + lightningPeriod;

				GameObject lightningObject = Instantiate(lightningPrefab, transform.position, Quaternion.identity) as GameObject;
				lightningObject.AddComponent<ThrowableObject>();
				lightningObject.GetComponent<ThrowableObject>().targetObject = targetObject;
				
			}
		}
		if(Time.time > nextEvilnessTime + evilnessTime)
		{
			GetComponent<Renderer>().material.SetColor("_BaseColor", new Color(1, 1, 1, 1));
			nextEvilnessTime = Time.time + evilnessPeriod;
		}
    }
}
