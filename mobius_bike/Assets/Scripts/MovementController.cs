using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    CharacterController controller;
    public float Speed = 5.0f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    

    void Update()
    {

        // Direction to travel vector3
        Vector3 direction = new Vector3(-1, 0, 0);
        //velocity = direction + speed
        Vector3 velocity = direction * Speed;
        // Move (velocity * timeDeltaTime)
        controller.Move(velocity * Time.deltaTime);
    }
}
