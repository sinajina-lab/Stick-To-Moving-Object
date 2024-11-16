using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physics_Rotation : MonoBehaviour
{
    public float speed = 100f;  // Rotation speed
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Apply rotation by adjusting the Rigidbody2D's angular velocity
        rb.angularVelocity = speed;
    }
}
