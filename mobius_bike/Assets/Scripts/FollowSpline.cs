using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpline : MonoBehaviour
{

    public CatmullRomSpline path;
    public float speed = 0.0001f;

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
        if (path == null)
            return;
				
		Vector3 pos = path.GetPosition(0);

        transform.position = pos;
    }

    void Update()
    {

		//Vector3 pos = path.GetPosition(transform, ref index, ref t);
		//transform.LookAt(pos);

		//transform.position += transform.TransformDirection(Vector3.forward).normalized * Time.deltaTime * speed;
		float s = Time.timeSinceLevelLoad * speed;
		transform.position = path.GetPosition(s);
		int index = Mathf.FloorToInt(s);
		transform.rotation = Quaternion.Lerp(path.GetPoint(index).rotation, path.GetPoint(index+1).rotation, s-index);
    }

}
