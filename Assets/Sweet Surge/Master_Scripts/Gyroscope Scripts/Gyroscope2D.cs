using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope2D : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;

    private float horizontalInput;

    [SerializeField] private float speed = 5f; // Adjustable in the Inspector

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal acceleration input without inverting direction
        horizontalInput = Input.acceleration.x * speed;

        // Clamp position within specified range
        float clampedX = Mathf.Clamp(transform.position.x, -7.5f, 7.5f);

        transform.position = new Vector2(clampedX, transform.position.y);
    }

    void FixedUpdate()
    {
        // Update Rigidbody2D velocity based on input
        Vector2 movement = new Vector2(horizontalInput, 0f);

        rigidBody2D.velocity = movement;
    }
}
