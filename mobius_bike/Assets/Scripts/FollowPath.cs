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
    private MovingPath currentPath;

    void Start()
    {
        if (path == null)
            return;

        currentPath = path;

        pointInPath = currentPath.GetNextPoint();

        pointInPath.MoveNext();

        if (pointInPath.Current == null)
            return;

        transform.position = pointInPath.Current.position;
    }

    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
            return;

        Vector3 leftRight = Vector3.zero;
        //прибавить к pointInPath.Current.position.x  
        if (Input.GetKey(KeyCode.A))
        {
            leftRight = Vector3.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            leftRight = Vector3.left;
        }

        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position + leftRight * 20, Time.deltaTime * speed);

        var distanceSquer = (transform.position - pointInPath.Current.position).sqrMagnitude; //достаточно ли близко к point

        transform.rotation = Quaternion.Lerp(transform.rotation, pointInPath.Current.rotation, rotationSpeed * Time.deltaTime);

        if (distanceSquer < max_disance * max_disance)
        {
            pointInPath.MoveNext();
            transform.LookAt(pointInPath.Current);
        }

        
    }
}
