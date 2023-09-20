using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPath : MonoBehaviour
{
    public int movingTo = 0;
    public Transform[] PathPoints;

    public void OnDrawGizmos()
    {
        if (PathPoints == null || PathPoints.Length < 2)
            return;

        for(int i = 1; i < PathPoints.Length; i++)
        {
            Gizmos.DrawLine(PathPoints[i - 1].position, PathPoints[i].position);
        }
        Gizmos.DrawLine(PathPoints[0].position, PathPoints[PathPoints.Length - 1].position);
    }

    public IEnumerator<Transform> GetNextPoint()
    {
        if (PathPoints == null || PathPoints.Length < 1)
        {
            yield break;
        }
        while (true)
        {
            yield return PathPoints[movingTo];
            if (PathPoints.Length == 1)
            {
                continue;
            }
            movingTo++;

            if(movingTo >= PathPoints.Length)
            {
                movingTo = 0;
            }
        }
    }
}
