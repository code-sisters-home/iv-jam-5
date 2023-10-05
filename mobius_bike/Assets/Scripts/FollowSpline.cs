using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FollowSpline : MonoBehaviour
{

    public CatmullRomSpline path;
    public float speed = 3f;
	public bool useControls = false;
	public float speedTurn = 3f;	
	
	public bool useForward = false;
	
	public float roadWidth = 5f;
	public float accStep = 0.1f;

	float shift = 0;
	float dist = 0;
	float acc = 0;
	//private IEnumerator<Transform> pointInPath;

	void OnDrawGizmos()
	{
		int index = 0;
		float t = 0;	
		//Vector3 pos = path.GetPosition(transform, ref index, ref t);
		//Gizmos.color = Color.cyan;
		//Gizmos.DrawLine(transform.position, pos);
		Gizmos.color = Color.yellow;
		Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward).normalized*5.0f);
		
	}

    void Start()
    {
		Assert.IsNotNull(path, "потерялся референс к дороге");
				
		Vector3 pos = path.GetPosition(0);

        transform.position = pos;
    }

    void Update()
    {
	
		bool isForward = Input.GetKey(KeyCode.W) && useControls;
		bool isBackward = Input.GetKey(KeyCode.S) && useControls;
		bool isLeft = Input.GetKey(KeyCode.A) && useControls;
		bool isRight = Input.GetKey(KeyCode.D) && useControls;
		
		if(isLeft)
			shift -= Time.deltaTime*speedTurn;
		else if(isRight)
			shift += Time.deltaTime*speedTurn;
		shift = Mathf.Clamp(shift, -1f, 1f);
		Vector3 vecTurn = transform.TransformDirection(shift * roadWidth * Vector3.right);
		
		if(useForward)
		{
			if(isForward)
				acc = Mathf.Clamp(acc + accStep, 0f, 1.0f);
			else if(isBackward)
				acc = Mathf.Clamp(acc - accStep, 0f, 1.0f);
			dist += acc * Time.deltaTime * speed;
		}
		else
			dist = Time.timeSinceLevelLoad * speed;
		
		PositionAndRotation pr = path.GetPositionAndRotation(dist); 
		transform.position = pr.position + vecTurn;
		transform.rotation = pr.rotation;

	}

}
