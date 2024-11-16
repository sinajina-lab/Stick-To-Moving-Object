using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationalMovement : MonoBehaviour
{
    public float rotationSpeed = 100f; // Player's rotation speed
    private Rigidbody2D rb;
    [SerializeField] private LayerMask planetLayer; // Layer for detecting planet collision

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Allow user to rotate when not colliding with a planet
        if (!IsCollidingWithPlanet())
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if (horizontalInput != 0)
            {
                RotateCharacter(horizontalInput);
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
}
