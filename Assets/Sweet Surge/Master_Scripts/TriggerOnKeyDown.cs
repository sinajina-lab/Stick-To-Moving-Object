using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnKeyDown : MonoBehaviour
{
    private CircleCollider2D candyplanet_collider; // Replace with your 2D collider type (e.g., CircleCollider2D)
    private bool keyPressed = false; // Flag to track if the key is already pressed

    void Start()
    {
        candyplanet_collider = GetComponent<CircleCollider2D>(); // Get the 2D collider component
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        //{
        //    // Toggle the isTrigger property of the 2D collider
        //    candyplanet_collider.isTrigger = !candyplanet_collider.isTrigger;
        //    Debug.Log("Down arrow or S key pressed. isTrigger set to " + candyplanet_collider.isTrigger);
        //}

        // Check if DownArrow or S is pressed
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!keyPressed)
            {
                // Toggle the isTrigger property of the 2D collider
                candyplanet_collider.isTrigger = true;
                Debug.Log("Down arrow or S key pressed. isTrigger set to true");
                keyPressed = true;
            }
        }
        // Check if DownArrow or S is released
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            candyplanet_collider.isTrigger = false; // Set isTrigger to false when either key is released
            Debug.Log("Down arrow or S key released. isTrigger set to false");
            keyPressed = false; // Reset the flag when the key is released
        }
    }
}
