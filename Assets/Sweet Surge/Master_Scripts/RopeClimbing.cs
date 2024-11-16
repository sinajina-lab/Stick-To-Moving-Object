using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeClimbing : MonoBehaviour
{
    [SerializeField] float climbSpeed = 2f; // Speed at which the player climbs up or down
    private bool isClimbing = false; // Flag to check if currently climbing
    private float currentRopeLength; // Current length of the rope
    private const float minRopeLength = 0.5f; // Minimum rope length
    private const float maxRopeLength = 10f; // Maximum rope length

    private GrapplingHook_2 grapplingHook; // Reference to the GrapplingHook_2 script

    private void Start()
    {
        grapplingHook = GetComponent<GrapplingHook_2>(); // Assuming GrapplingHook_2 is on the same GameObject
    }

    public void StartClimbing(float ropeLength)
    {
        isClimbing = true;
        currentRopeLength = ropeLength;
    }

    public void StopClimbing()
    {
        isClimbing = false;
    }

    private void Update()
    {
        if (isClimbing)
        {
            HandleClimbing();
        }
    }

    private void HandleClimbing()
    {
        // Climb Up
        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.UpArrow))
        {
            currentRopeLength = Mathf.Clamp(currentRopeLength - climbSpeed * Time.deltaTime, minRopeLength, maxRopeLength);
            UpdateRopePosition();
        }
        // Climb Down
        else if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.DownArrow))
        {
            currentRopeLength = Mathf.Clamp(currentRopeLength + climbSpeed * Time.deltaTime, minRopeLength, maxRopeLength);
            UpdateRopePosition();
        }

        // Stop climbing if out of bounds
        if (currentRopeLength <= minRopeLength || currentRopeLength >= maxRopeLength)
        {
            StopClimbing();
        }
    }

    private void UpdateRopePosition()
    {
        if (grapplingHook != null)
        {
            grapplingHook.UpdateRopeLength(currentRopeLength); // Update the rope length in the grappling hook script
        }
    }
}
