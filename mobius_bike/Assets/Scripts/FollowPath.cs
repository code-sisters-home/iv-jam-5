using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public MovingPath path;
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public float max_disance = 0.1f;

    private IEnumerator<Transform> pointInPath;

    void Start()
    {
        if (path == null)
            return;
        pointInPath = path.GetNextPoint();

        pointInPath.MoveNext();

        if (pointInPath.Current == null)
            return;

        transform.position = pointInPath.Current.position;
    }

    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);

        var distanceSquer = (transform.position - pointInPath.Current.position).sqrMagnitude; //достаточно ли близко к point

        //Vector3 targetDirection = pointInPath.Current.position - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, pointInPath.Current.rotation, rotationSpeed * Time.deltaTime);

        if (distanceSquer < max_disance * max_disance)
        {
            pointInPath.MoveNext();
            transform.LookAt(pointInPath.Current);
        }
    }
}
