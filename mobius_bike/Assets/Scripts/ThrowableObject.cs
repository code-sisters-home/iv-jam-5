using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public GameObject targetObject;	
	private float nextTime = 0.0f;
    public float period = 0.09f;
	public float speed = 10.0f;
	
	private Space initSpace;
	private Transform target;
    // Start is called before the first frame update
    void Start()
    {
		target = targetObject.transform;
		transform.LookAt(target.position);	
		initSpace = Space.Self;
    }

    // Update is called once per frame
    void Update()
    {
		if(target != targetObject.transform)
		{	
			target = targetObject.transform;
			transform.LookAt(target.position);	
			initSpace = Space.Self;				
		}		
		
        if (Time.time > nextTime)
        {
            nextTime = Time.time + period;
			transform.Rotate(0, 0, Random.Range(0.0f, 180.0f), initSpace);
		}		
		
	    var step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);	


	 if (Vector3.Distance(transform.position, target.position) < 0.001f)
        {
            // Swap the position of the cylinder.
            Destroy(gameObject);
        }		
		
    }
}
