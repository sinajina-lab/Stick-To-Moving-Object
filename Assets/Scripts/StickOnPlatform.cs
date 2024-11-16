using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOnPlatform : MonoBehaviour
{
    // Ensures that the player becomes a child of the platform on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Use tags instead of names for better reliability
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // Handles player detachment from the platform when leaving collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
