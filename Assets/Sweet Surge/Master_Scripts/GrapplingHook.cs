using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LineRenderer ropeRenderer; // The rope that connects player and target planet
    public Transform player; // Player object
    public Transform targetPlanet; // Target planet object
    [SerializeField] bool isClimbing = false; // Is the player currently climbing?
    private float currentRopeLength; // The current length of the rope
    public float climbSpeed = 2f; // Speed at which the player climbs up or down

    [SerializeField] float minRopeLength = 0.5f; // Minimum rope length
    [SerializeField] float maxRopeLength = 10f; // Maximum rope length
    [SerializeField] bool playerOnPlanet = false; // Detect if the player is on the planet

    void Update()
    {
        // Start climbing down the rope
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            StartClimbingDown();
        }
        // Start climbing up the rope
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            StartClimbingUp();
        }

        // Handle climbing movement and rope display
        HandleClimbing();
    }

    void StartClimbingDown()
    {
        if (playerOnPlanet) return; // Don't start climbing if the player is on the planet
        isClimbing = true;
        player.GetComponent<Attractable_Player>().SetClimbing(true);
        currentRopeLength = Vector2.Distance(player.position, targetPlanet.position);
        ropeRenderer.enabled = true; // Display the rope when climbing starts
    }

    void StartClimbingUp()
    {
        if (playerOnPlanet) return; // Don't start climbing if the player is on the planet
        isClimbing = true;
        player.GetComponent<Attractable_Player>().SetClimbing(true);
        currentRopeLength = Vector2.Distance(player.position, targetPlanet.position);
        ropeRenderer.enabled = true; // Display the rope when climbing starts
    }

    void HandleClimbing()
    {
        if (!isClimbing) return;

        // Climb up the rope
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            currentRopeLength = Mathf.Clamp(currentRopeLength - climbSpeed * Time.deltaTime, minRopeLength, maxRopeLength);
        }
        // Climb down the rope
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            currentRopeLength = Mathf.Clamp(currentRopeLength + climbSpeed * Time.deltaTime, minRopeLength, maxRopeLength);
        }

        // Update the rope renderer positions
        ropeRenderer.SetPosition(0, player.position); // Player's position
        ropeRenderer.SetPosition(1, targetPlanet.position); // Target planet's position

        // Stop climbing when the player reaches the maximum or minimum rope length
        if (currentRopeLength <= minRopeLength || currentRopeLength >= maxRopeLength)
        {
            StopClimbing();
        }
    }

    void StopClimbing()
    {
        isClimbing = false;
        player.GetComponent<Attractable_Player>().SetClimbing(false);
        ropeRenderer.enabled = false; // Hide the rope when climbing stops
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the planet, stop climbing and hide the rope
        if (collision.transform == targetPlanet)
        {
            playerOnPlanet = true;
            StopClimbing(); // Stop climbing when colliding with the planet
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // When the player leaves the planet, reset the playerOnPlanet flag
        if (collision.transform == targetPlanet)
        {
            playerOnPlanet = false;
        }
    }
}
