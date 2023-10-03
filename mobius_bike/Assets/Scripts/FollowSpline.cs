using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FollowSpline : MonoBehaviour
{

    public CatmullRomSpline path;
    public float speed = 3f;
	public float speedTurn = 3f;

	bool isLeft = false;
	bool isRight = false;

	Vector3 vecTurn = Vector3.zero;
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

		//Vector3 pos = path.GetPosition(transform, ref index, ref t);
		//transform.LookAt(pos);

		
		isLeft = Input.GetKey(KeyCode.A);
		isRight = Input.GetKey(KeyCode.D);
		if (Input.GetKey(KeyCode.A))
			vecTurn = transform.TransformDirection(Vector3.left);
		else if (Input.GetKey(KeyCode.D))
			vecTurn = transform.TransformDirection(Vector3.right);
		else
			vecTurn = Vector3.zero;

		//transform.position += transform.TransformDirection(Vector3.forward).normalized * Time.deltaTime * speed;
		float s = Time.timeSinceLevelLoad * speed;
		transform.position = path.GetPosition(s) + vecTurn * speedTurn;
		int index = Mathf.FloorToInt(s);
		transform.rotation = Quaternion.Lerp(path.GetPoint(index).rotation, path.GetPoint(index+1).rotation, s-index);

	}

}
