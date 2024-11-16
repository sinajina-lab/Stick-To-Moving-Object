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

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void Start()
    {
        if (landedNutsText == null)
        {
            Debug.LogWarning("landedNutsText is not assigned in the Inspector.");
        }
        landedNutsText.text = "" + collectable;
    }

    public void IncreasePoints()
    {
        collectable++;
        landedNutsText.text = "" + collectable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        CandyPlanet candyPlanet = collision.gameObject.GetComponent<CandyPlanet>();

        if (candyPlanet != null && !candyPlanet.HasBeenVisited)
        {
            IncreasePoints();
            candyPlanet.HasBeenVisited = true;  // Mark this planet as visited
            Debug.Log("Candy Planet: " + collectable);
        }
    }

    public class CandyPlanet : MonoBehaviour
    {
        public bool HasBeenVisited { get; set; } = false;
    }
}
