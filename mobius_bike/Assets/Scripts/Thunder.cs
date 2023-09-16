using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;

            GameObject lightningObject = Instantiate(lightning, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
