using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollCamera : MonoBehaviour
{
    [SerializeField] private Transform player; // Reference to the player
    [SerializeField] private float scrollSpeed = 2f; // Speed of camera auto-scroll to the right
    [SerializeField] private float leftBoundary = -5f; // Left boundary relative to the camera's position
    [SerializeField] private GameObject gameOverPanel; // Reference to the Game Over panel

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Ensure the Game Over panel is hidden at start
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Move the camera steadily to the right
        transform.position += Vector3.right * scrollSpeed * Time.deltaTime;

        // Check if the player is out of bounds
        float leftLimit = transform.position.x + leftBoundary;
        if (player.position.x < leftLimit)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Show the Game Over panel
            Time.timeScale = 0f; // Pause the game
        }
    }
}
