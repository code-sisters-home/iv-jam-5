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
	public float accStep = 0.01f;

	float shift = 0;
	float dist = 0;
	float acc = 0;
	float rot = 0;
	float sign = 1;
	
	void OnDrawGizmos()
	{
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
		{
			shift -= Time.deltaTime*speedTurn;
			rot += Time.deltaTime*speedTurn;
		}
		else if(isRight)
		{
			shift += Time.deltaTime*speedTurn;
			rot -= Time.deltaTime*speedTurn;
		}
		else if(Mathf.Abs(rot - Mathf.Sign(rot)*Time.deltaTime ) < Mathf.Abs(rot))
		{
			rot -= Mathf.Sign(rot)*Time.deltaTime;
		}
		shift = Mathf.Clamp(shift, -1f, 1f);
		rot = Mathf.Clamp(rot, -1f, 1f);
		
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
		transform.rotation = pr.rotation;
		
		Vector3 vecTurn = transform.TransformDirection(shift * roadWidth * Vector3.right);
		transform.position = pr.position + vecTurn;		
		transform.Rotate(0.0f, 0.0f, rot*45.0f, Space.Self);
	}

}
