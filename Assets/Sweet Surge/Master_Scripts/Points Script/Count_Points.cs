using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Count_Points : MonoBehaviour
{
    public static Count_Points instance;  // Singleton instance

    [SerializeField] int collectable = 0;
    [SerializeField] TMP_Text landedNutsText;
    [SerializeField] LayerMask candyPlanetLayer; // LayerMask for Candy Planet

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (landedNutsText == null)
        {
            Debug.LogError("landedNutsText is not assigned in the Inspector!");
            return;
        }
        landedNutsText.text = "" + collectable;
    }

    public void IncreasePoints()
    {
        collectable++;
        Debug.Log("Points increased to: " + collectable);

        if (landedNutsText == null)
        {
            Debug.LogError("landedNutsText is not assigned when trying to update text!");
            return;
        }

        landedNutsText.text = "" + collectable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        // Check if the collided object is in the Candy Planet layer
        if ((candyPlanetLayer & (1 << collision.gameObject.layer)) != 0)
        {
            // Get the CandyPlanet component directly from the collided object
            CandyPlanet candyPlanet = collision.gameObject.GetComponentInChildren<CandyPlanet>();

            if (candyPlanet == null)
            {
                Debug.LogError("CandyPlanet component is missing on " + collision.gameObject.name);
                return;
            }

            Debug.Log("HasBeenVisited for " + collision.gameObject.name + ": " + candyPlanet.HasBeenVisited);

            // Award points if the platform hasn't been visited
            if (!candyPlanet.HasBeenVisited)
            {
                IncreasePoints();
                candyPlanet.HasBeenVisited = true; // Mark platform as visited
                Debug.Log("Points awarded to: " + collision.gameObject.name);
            }
            else
            {
                Debug.Log("Already visited: " + collision.gameObject.name);
            }
        }
    }
}
