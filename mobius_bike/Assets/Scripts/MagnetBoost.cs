using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBoost : MonoBehaviour
{
    public float magnetStrength = 10f;

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("drop"))
        {
            Vector3 direction = transform.position - other.transform.position;
            other.transform.position += direction.normalized * magnetStrength * Time.deltaTime;
        }
    }
}
