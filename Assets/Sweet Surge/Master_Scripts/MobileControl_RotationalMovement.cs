using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControl_RotationalMovement : MonoBehaviour
{
    public float rotationSpeed = 100f; // Player's rotation speed
    private Rigidbody2D rb;
    [SerializeField] private LayerMask planetLayer; // Layer for detecting planet collision

    private bool rotateRight = false;
    private bool rotateLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Allow user to rotate when not colliding with a planet
        if (IsCollidingWithPlanet())
        {
            // Handle input for PC (keyboard)
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0)
            {
                RotateCharacter(horizontalInput);
            }

            // Handle rotation for mobile based on button presses
            if (rotateRight)
            {
                RotateCharacter(1); // Rotate right
            }
            else if (rotateLeft)
            {
                RotateCharacter(-1); // Rotate left
            }
        }
    }

    private void RotateCharacter(float direction)
    {
        // Rotate based on input direction
        transform.Rotate(Vector3.forward, -direction * rotationSpeed * Time.deltaTime);
    }

    private bool IsCollidingWithPlanet()
    {
        // Raycast downwards to check for planet collision using LayerMask
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, planetLayer);
        return hit.collider != null;
    }

    // These methods are called by the UI buttons
    public void OnRightButtonDown()
    {
        rotateRight = true;
    }

    public void OnRightButtonUp()
    {
        rotateRight = false;
    }

    public void OnLeftButtonDown()
    {
        rotateLeft = true;
    }

    public void OnLeftButtonUp()
    {
        rotateLeft = false;
    }
}
