using UnityEngine;
using System.Collections;

//https://www.habrador.com/tutorials/interpolation/1-catmull-rom-splines/
//Interpolation between points with a Catmull-Rom spline
public class PositionAndRotation
{
	public PositionAndRotation()
	{
		position = new Vector3(0, 0, 0);
		rotation =  Quaternion.identity;
		
	}
	public Vector3 position {get; set;}
	public Quaternion rotation {get; set;}
}	


public class CatmullRomSpline : MonoBehaviour
{

	//Has to be at least 4 points
	public Transform[] controlPointsList;
	//Are we making a line or a loop?
	public bool isLooping = true;
	
	private int counter = 0;

	//Display without having to press play
	void OnDrawGizmos()
	{
		Gizmos.color = Color.white;

		//Draw the Catmull-Rom spline between the points
		for (int i = 0; i < controlPointsList.Length; i++)
		{
			Gizmos.DrawSphere(controlPointsList[i].position, 0.3f);
			//Cant draw between the endpoints
			//Neither do we need to draw from the second to the last endpoint
			//...if we are not making a looping line
			if ((i == 0 || i == controlPointsList.Length - 2 || i == controlPointsList.Length - 1) && !isLooping)
			{
				continue;
			}

			DisplayCatmullRomSpline(i);
		}
	}

	//Display a spline between 2 points derived with the Catmull-Rom spline algorithm
	void DisplayCatmullRomSpline(int pos)
	{
		//The 4 points we need to form a spline between p1 and p2
		Vector3 p0 = controlPointsList[ClampListPos(pos - 1)].position;
		Vector3 p1 = controlPointsList[pos].position;
		Vector3 p2 = controlPointsList[ClampListPos(pos + 1)].position;
		Vector3 p3 = controlPointsList[ClampListPos(pos + 2)].position;

		//The start position of the line
		Vector3 lastPos = p1;

		//The spline's resolution
		//Make sure it's is adding up to 1, so 0.3 will give a gap, but 0.2 will work
		float resolution = 0.2f;

		//How many times should we loop?
		int loops = Mathf.FloorToInt(1f / resolution);

		for (int i = 1; i <= loops; i++)
		{
			//Which t position are we at?
			float t = i * resolution;

			//Find the coordinate between the end points with a Catmull-Rom spline
			Vector3 newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);

			//Draw this line segment
			Gizmos.DrawLine(lastPos, newPos);

			//Save this pos so we can draw the next line segment
			lastPos = newPos;
		}
	}

	public PositionAndRotation GetPositionAndRotation(float s)
	{
		PositionAndRotation positionAndRotation = new PositionAndRotation();		
		positionAndRotation.position = GetPosition(s);
		int index = Mathf.FloorToInt(s);
		positionAndRotation.rotation = Quaternion.Lerp(GetPoint(index).rotation, GetPoint(index+1).rotation, s-index);
		return positionAndRotation;
	}

	public Vector3 GetPosition(float s)
	{
		int index = Mathf.FloorToInt(s);
		float t = s - index;
		Vector3 p0 = controlPointsList[ClampListPos(index - 1)].position;
		Vector3 p1 = controlPointsList[ClampListPos(index)].position;
		Vector3 p2 = controlPointsList[ClampListPos(index + 1)].position;
		Vector3 p3 = controlPointsList[ClampListPos(index + 2)].position;

		return GetCatmullRomPosition(t, p0, p1, p2, p3);
	}

	public Vector3 GetPosition(Transform obj, ref int index, ref float t)
	{
		index = counter;
		Vector3 p0 = controlPointsList[ClampListPos(counter - 1)].position;
		Vector3 p1 = controlPointsList[ClampListPos(counter)].position;
		Vector3 p2 = controlPointsList[ClampListPos(counter + 1)].position;
		Vector3 p3 = controlPointsList[ClampListPos(counter + 2)].position;

		int iterations = 32;
		float t0 = 0;
		float t1 = 1;
		Vector3 pos = p1;
		for (int i = 0; i < iterations; i++)
		{
			Vector3 pos0 = GetCatmullRomPosition(t0, p0, p1, p2, p3);
			Vector3 pos1 = GetCatmullRomPosition(t1, p0, p1, p2, p3);
			float dist0 = Vector3.Distance(obj.position, pos0);
			float dist1 = Vector3.Distance(obj.position, pos1);
			if(dist0 > dist1)
			{
				pos = pos1;
				t = t1;
				t0 = 0.5f*(t1-t0) + t0;
			}
			else
			{
				pos = pos0;
				t = t0;
				t1 = t1 - 0.5f*(t1-t0);
				
			}
		}
		float eps = 0.0025f;
		
		if(1 - t < eps)
		{
			counter++;
		}
		
		return pos;		
	}

	public PositionAndRotation GetClosestPositionAndRotation(Transform obj)
	{
		PositionAndRotation positionAndRotation = new PositionAndRotation();		
		int index = 0;
		float t = 0;
		positionAndRotation.position = GetClosestPosition(obj, ref index, ref t);
		positionAndRotation.rotation = Quaternion.Lerp(GetPoint(index).rotation, GetPoint(index+1).rotation, t);
		return positionAndRotation;
	}

	public Vector3 GetClosestPosition(Transform obj)
	{
		int index = 0;
		float t = 0;
		return GetClosestPosition(obj, ref index, ref t);
	}

	public Vector3 GetClosestPosition(Transform obj, ref int closestIndex, ref float t)
	{
		closestIndex = 	GetClosestIndex(obj, true);
		Vector3 p0 = controlPointsList[ClampListPos(closestIndex - 2)].position;
		Vector3 p1 = controlPointsList[ClampListPos(closestIndex - 1)].position;
		Vector3 p2 = controlPointsList[ClampListPos(closestIndex)].position;
		Vector3 p3 = controlPointsList[ClampListPos(closestIndex + 1)].position;

		int iterations = 32;
		float t0 = 0;
		float t1 = 1;
		Vector3 pos = p1;
		for (int i = 0; i < iterations; i++)
		{
			Vector3 pos0 = GetCatmullRomPosition(t0, p0, p1, p2, p3);
			Vector3 pos1 = GetCatmullRomPosition(t1, p0, p1, p2, p3);
			float dist0 = Vector3.Distance(obj.position, pos0);
			float dist1 = Vector3.Distance(obj.position, pos1);
			if(dist0 > dist1)
			{
				pos = pos1;
				t = t1;
				t0 = 0.5f*(t1-t0) + t0;
			}
			else
			{
				pos = pos0;
				t = t0;
				t1 = t1 - 0.5f*(t1-t0);
				
			}
		}
		
		return pos;		
	}
	
	int GetClosestIndex(Transform obj, bool lookForward = true)
	{
		int closestPointIndex = -1;
		float minDist = Mathf.Infinity;
		for (int i = 0; i < controlPointsList.Length; i++)
		{
			if ((i == 0 || i == controlPointsList.Length - 2 || i == controlPointsList.Length - 1) && !isLooping)
			{
				continue;
			}
			
			Transform point = controlPointsList[i];
			Vector3 forward = obj.TransformDirection(Vector3.forward);
            Vector3 toPoint = point.position - obj.position;
	
			bool behind = Vector3.Dot(forward, toPoint) < 0;
			
			if(lookForward && !behind || !lookForward)
			{
				float dist = Vector3.Distance(obj.position, point.position);
				if(dist < minDist)
				{
					minDist = dist;
					closestPointIndex = i;					
				}
			}
		}
		if(closestPointIndex == -1)
			GetClosestIndex(obj, false);
		return closestPointIndex;
	}	
	
	
	public Transform GetPoint(int i)
	{
		return controlPointsList[ClampListPos(i)];
	}	
	
	//Clamp the list positions to allow looping
	int ClampListPos(int pos)
	{
		pos = (pos + controlPointsList.Length) % controlPointsList.Length;

		return pos;
	}

	//Returns a position between 4 Vector3 with Catmull-Rom spline algorithm
	//http://www.iquilezles.org/www/articles/minispline/minispline.htm
	Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		//The coefficients of the cubic polynomial (except the 0.5f * which I added later for performance)
		Vector3 a = 2f * p1;
		Vector3 b = p2 - p0;
		Vector3 c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
		Vector3 d = -p0 + 3f * p1 - 3f * p2 + p3;

		//The cubic polynomial: a + b * t + c * t^2 + d * t^3
		Vector3 pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

		return pos;
	}
}