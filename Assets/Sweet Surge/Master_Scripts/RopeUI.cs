using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeUI : MonoBehaviour
{
    [SerializeField] private Slider ropeLengthSlider; // UI Slider to show rope length
    [SerializeField] private float maxRopeLength = 10f; // Maximum length of the rope
    [SerializeField] private float minRopeLength = 0.5f; // Minimum length of the rope
    [SerializeField] private float currentRopeLength; // Current length of the rope
    [SerializeField] private float climbSpeed = 2f; // Speed of climbing

    private bool isClimbing = false; // Flag to track if the player is climbing

    void Start()
    {
        // Initialize the rope length
        currentRopeLength = maxRopeLength;
        UpdateRopeUI();
    }

    void Update()
    {
        HandleClimbingInput();
        UpdateRopeUI(); // Update the UI in every frame
    }

    void HandleClimbingInput()
    {
        // Check for climbing down (DownArrow or S)
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (!isClimbing)
            {
                isClimbing = true;
                StartClimbingDown();
            }
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            StopClimbing();
        }

        // Check for climbing up (UpArrow or W)
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (!isClimbing)
            {
                isClimbing = true;
                StartClimbingUp();
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            StopClimbing();
        }
    }

    void StartClimbingDown()
    {
        // Reduce rope length when climbing down
        currentRopeLength = Mathf.Clamp(currentRopeLength + climbSpeed * Time.deltaTime, minRopeLength, maxRopeLength);
    }

    void StartClimbingUp()
    {
        // Increase rope length when climbing up
        currentRopeLength = Mathf.Clamp(currentRopeLength - climbSpeed * Time.deltaTime, minRopeLength, maxRopeLength);
    }

    void StopClimbing()
    {
        isClimbing = false;
    }

    void UpdateRopeUI()
    {
        // Update the slider value to match the current rope length
        if (ropeLengthSlider != null)
        {
            ropeLengthSlider.value = currentRopeLength / maxRopeLength;
        }
    }
}
