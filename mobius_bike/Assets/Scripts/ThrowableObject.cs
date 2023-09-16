using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
	  private float nextTime = 0.0f;
    public float period = 0.09f;
	private Space initSpace;
    // Start is called before the first frame update
    void Start()
    {
        initSpace = Space.Self;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + period;
			transform.Rotate(0, Random.Range(0.0f, 180.0f), 0, initSpace);
		}
    }
}
