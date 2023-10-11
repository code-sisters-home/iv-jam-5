using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
	public PositionAndRotation positionAndRotation = new PositionAndRotation();
	public float lifetime = 20.0f;
	public float speed = 0.1f;
	
	//bool oscillations = false;
	//private float initTime;
	public float roadUsableWidth = 3.0f;
	public Vector2 shift = Vector2.zero;
	
	private float t = 0;
	private float heightCoeff = 0;
	private bool oscillations = false;
	public float oscillationSpeed = 1f;
	private Quaternion rotation = Quaternion.identity;
	private Vector3 position = Vector3.zero;
	private Vector3 targetPos = Vector3.zero;
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, positionAndRotation.position);
		Gizmos.color = Color.green;
		
		Quaternion r = transform.rotation;
		transform.rotation = positionAndRotation.rotation;
		Vector3 up = transform.TransformDirection(Vector3.up).normalized;
		transform.rotation = r;
		Gizmos.DrawRay(positionAndRotation.position, up*5);	
	}
	
    // Start is called before the first frame update
    void Start()
    {
		//initTime = Time.time;
		Destroy(gameObject, lifetime);
		
		heightCoeff = (transform.position.y -  positionAndRotation.position.y)*0.05f;
		
		transform.rotation = positionAndRotation.rotation;
		Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
		Vector3 right = transform.TransformDirection(Vector3.right).normalized;
		rotation.SetLookRotation(forward);
		
		transform.rotation = rotation;
		
		targetPos = positionAndRotation.position + Vector3.Project(new Vector3(shift.x, 0, shift.y), right);

    }

	void dampedOscillations()
	{
		t = Mathf.Clamp(t, 0, 1);
		
		float h = Mathf.Exp(-0.9f*t)*(Mathf.Sin(Mathf.PI*(2f*t-0.5f))+1);
		float w = t;
	
		transform.rotation = positionAndRotation.rotation;
		Vector3 right = transform.TransformDirection(Vector3.right).normalized*w;
		right = right*Mathf.Sign(-right.y)*roadUsableWidth*0.3f;
		Vector3 up = transform.TransformDirection(Vector3.up).normalized*h*heightCoeff;
		
		Vector3 v = up + right;
		
		transform.rotation = Quaternion.Lerp(rotation, positionAndRotation.rotation, t);
		transform.position = position + v;
		
		t+=Time.deltaTime*oscillationSpeed;
	}

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > targetPos.y)
			transform.position += Vector3.down * speed;
		else if(!oscillations)
		{
			oscillations = true;
			position = targetPos;
		}
		if(oscillations)
			dampedOscillations();

        //if (Time.time > initTime + lifetime)
        //{
        //    Destroy(gameObject);
        //}		

    }
}
