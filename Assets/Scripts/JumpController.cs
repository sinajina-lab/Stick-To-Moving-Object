using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Jump System")]
    [SerializeField] float jumpTime = 0.5f;     // Duration of the jump's upward force
    [SerializeField] int jumpPower = 10;        // Initial jump velocity
    [SerializeField] float fallMultiplier = 2.5f;  // Multiplier to increase falling speed
    [SerializeField] float jumpMultiplier = 1.5f;  // Multiplier for the rising speed

    public Transform groundCheck;
    public LayerMask groundLayer;

    Vector2 vecGravity;  // Will be a negative value, affecting downward movement

    bool isJumping = false;    // To track if the player is still jumping
    float jumpCounter = 0f;    // To time the jump duration

    // Start is called before the first frame update
    void Start()
    {
        vecGravity = new Vector2(0, Physics2D.gravity.y);  // This is usually a negative vector
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle the jump initiation
        if (Input.GetMouseButtonDown(0) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);  // Initial jump impulse
            isJumping = true;
            jumpCounter = 0f;
        }

        // Handle the jump sustain (if the mouse button is still held)
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;  // End jump when max time is reached

            // Adjust jump multiplier over time, reducing the force in the latter half
            float t = jumpCounter / jumpTime;  // Proportion of jump time passed
            float currentJumpM = jumpMultiplier;

            if (t > 0.5f)  // Reduce jump force in the latter half
                currentJumpM = jumpMultiplier * (1 - t);

            rb.velocity += vecGravity * currentJumpM * Time.deltaTime;  // Apply sustained upward force
        }

        // Handle jump cut short (when the mouse button is released)
        if (Input.GetMouseButtonUp(0))
        {
            isJumping = false;  // Stop jumping when the button is released
            jumpCounter = 0f;

            // Reduce the upward velocity to create a jump cut-off effect
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.6f);
            }
        }

        // Handle falling by increasing gravity when the player is falling
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;  // Increase the downward force when falling
        }
    }

    // Method to check if the player is grounded
    bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
}
