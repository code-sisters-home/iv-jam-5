using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    public MovingPath path;
    public float speed = 1;
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

        if(distanceSquer < max_disance * max_disance)
        {
            pointInPath.MoveNext();
        }
    }
}
