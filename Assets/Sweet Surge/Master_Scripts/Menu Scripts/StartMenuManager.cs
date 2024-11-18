using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuPanel; // Reference to Start Menu panel
    [SerializeField] private GameObject planetScoreText; // Reference to the planet score points text (TMPRO)
    [SerializeField] private GameObject coinPointsText; // Reference to the coin points text (TMPRO)
    [SerializeField] private AutoScrollCamera autoScrollCamera; // Reference to the AutoScrollCamera script
    [SerializeField] private TrailRenderer playerTrail; // Reference to the Trail Renderer on the player

    void Start()
    {
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(true); // Show the Start Menu panel at the beginning
        }

        // Ensure the score and coin texts are inactive at the start
        if (planetScoreText != null) planetScoreText.SetActive(false);
        if (coinPointsText != null) coinPointsText.SetActive(false);

        // Disable AutoScrollCamera and Trail Renderer at the start
        if (autoScrollCamera != null) autoScrollCamera.enabled = false;
        if (playerTrail != null) playerTrail.enabled = false;
    }

    // Method to start the game
    public void StartGame()
    {
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(false); // Hide the Start Menu panel
        }

        // Activate the score and coin points text
        if (planetScoreText != null) planetScoreText.SetActive(true);
        if (coinPointsText != null) coinPointsText.SetActive(true);

        // Enable AutoScrollCamera and Trail Renderer
        if (autoScrollCamera != null) autoScrollCamera.enabled = true;
        if (playerTrail != null) playerTrail.enabled = true;

        Debug.Log("Game Started! Components activated.");
    }
}
