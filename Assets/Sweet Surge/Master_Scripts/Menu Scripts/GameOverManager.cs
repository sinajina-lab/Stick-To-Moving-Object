using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject startMenuPanel; // Reference to Start Menu panel

    public void RestartLevel()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void WatchAdvert()
    {
        // Play an advert here (placeholder logic)
        Debug.Log("Playing Advert...");
        Time.timeScale = 1f; // Resume the game after advert
        gameObject.SetActive(false); // Hide the Game Over panel
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        if (startMenuPanel != null)
        {
            startMenuPanel.SetActive(true); // Show the Start Menu panel
        }
        gameObject.SetActive(false); // Hide the Game Over panel
    }
}
