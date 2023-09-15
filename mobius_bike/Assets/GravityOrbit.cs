using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float Gravity;
    public bool FixedDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GravityControlScript>())
            other.GetComponent<GravityControlScript>().Gravity = this.GetComponent<GravityOrbit>();
    }
}
