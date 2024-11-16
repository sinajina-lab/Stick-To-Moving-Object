using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public string StuckObjectTag = "Nut";
    public float stickForce = 10f;
    public Transform groundCheck; // Reference to a position for checking proximity to the rotating platform
    public Vector2 capsuleSize = new Vector2(1f, 0.1f); // Size of the capsule for detection
    public LayerMask groundLayer; // Layer for detecting if we are close to the platform

    private Rigidbody2D rb;
    private _RotationTest rotatingScript;
    private bool isStuck = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check if the player is close to a platform tagged with StuckObjectTag
        isStuck = IsNearRotatingPlatform();

        if (isStuck && rotatingScript != null)
        {
            // Calculate the tangential velocity of the rotating object at the player's position
            Vector3 directionToCenter = rotatingScript.transform.position - transform.position;
            float rotationalSpeed = rotatingScript.speed * Mathf.Deg2Rad;
            Vector3 tangentialVelocity = Vector3.Cross(rotatingScript.rotationAxis, directionToCenter).normalized * rotationalSpeed * directionToCenter.magnitude;

            // Apply a force to match the tangential velocity, converting to 2D
            Vector2 forceToApply = (Vector2)tangentialVelocity - rb.velocity;
            rb.AddForce(forceToApply * stickForce);
        }
    }

    private bool IsNearRotatingPlatform()
    {
        // Check for nearby colliders using OverlapCapsule
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(groundCheck.position, capsuleSize, CapsuleDirection2D.Horizontal, 0, groundLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(StuckObjectTag))
            {
                rotatingScript = collider.GetComponent<_RotationTest>();
                return true;
            }
        }

        rotatingScript = null;
        return false;
    }

    // Gizmos for visualizing the capsule area in the Scene view
    private void OnDrawGizmos()
    {
        // Draw the capsule in the Scene view to visualize the detection range
        if (groundCheck != null)
        {
            Gizmos.color = Color.green; // Set color to green for the capsule
            Gizmos.DrawWireSphere(groundCheck.position, capsuleSize.x / 2); // Capsule representation

            // Optionally, show the direction of the capsule's check
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * capsuleSize.y);
        }
    }
}
