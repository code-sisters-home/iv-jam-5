using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitObjects : MonoBehaviour
{
    public GameObject obj;
	public CatmullRomSpline landingPath;
	
	private float nextSpawnTime = 0.0f;
    public float emitPeriod = 2f;

	private float roadUsableWidth = 3.0f;
	private PositionAndRotation positionAndRotation = new PositionAndRotation();
    // Start is called before the first frame update
	private Vector2 shift = Vector2.zero;
	
	void OnDrawGizmos()
	{
		PositionAndRotation pr = landingPath.GetClosestPositionAndRotation(transform);		
		
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, pr.position);
		
	}	
	
    void Start()
    {
        
    }
	
	bool checkEmit()
	{
		positionAndRotation = landingPath.GetClosestPositionAndRotation(transform);
		Vector2 thisXZ = new Vector2(transform.position.x, transform.position.z);
		Vector2 pathXZ = new Vector2(positionAndRotation.position.x, positionAndRotation.position.z);
		
		obj.transform.rotation = positionAndRotation.rotation;
		float slope = obj.transform.TransformDirection(new Vector3(0, 1, 0)).y;
		obj.transform.rotation = Quaternion.identity;
		
		bool slopeIsAcceptable = slope > 0.5;
		//Debug.Log(slope);
		//if(slopeIsAcceptable)
			//Debug.Log("slopeIsAcceptable: " + slope);
		shift = thisXZ - pathXZ;
		bool pathIsUnder = Vector2.Distance(thisXZ, pathXZ) < roadUsableWidth*slope;
		
		//Debug.Log(Vector2.Distance(thisXZ, pathXZ));
		//if(pathIsUnder)
			//Debug.Log("pathIsUnder: ");	
		
		if(slopeIsAcceptable && pathIsUnder)
		{
			if (Time.time < nextSpawnTime)
				return false;
			nextSpawnTime = Time.time + emitPeriod;
			return true;
		}
		return false;
	}

    // Update is called once per frame
    void Update()
    {
		if (checkEmit())
		{
				GameObject fallingObject = Instantiate(obj, transform.position, Quaternion.identity) as GameObject;
				fallingObject.AddComponent<FallingObject>();
				fallingObject.GetComponent<FallingObject>().roadUsableWidth = roadUsableWidth;
				fallingObject.GetComponent<FallingObject>().shift = shift;
				fallingObject.GetComponent<FallingObject>().positionAndRotation = positionAndRotation;
				
		}
    }
}
