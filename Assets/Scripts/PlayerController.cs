using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float targetDistanceThreshold = 5f;
    public float launchTimer = 2f;
    public float launchForce = 100f;
    public float gravityForce = 10f;

    private Rigidbody2D rb;
    private float launchTimerRemaining;
    private GameObject targetPlanet;
    public bool isLaunched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Find the nearest planet within range
        GameObject nearestPlanet = FindNearestPlanetInRange();

        if (nearestPlanet != null)
        {
            // Start or continue the launch timer
            launchTimerRemaining -= Time.deltaTime;

            if (launchTimerRemaining <= 0 || Vector2.Distance(transform.position, nearestPlanet.transform.position) < targetDistanceThreshold)
            {
                // Launch towards the target planet
                Launch(nearestPlanet);
            }
            else
            {
                // Apply gravity force towards the target planet
                ApplyGravityForce(nearestPlanet);
            }
        }
    }

    GameObject FindNearestPlanetInRange()
    {
        GameObject nearestPlanet = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            float distance = Vector2.Distance(transform.position, planet.transform.position);
            if (distance < targetDistanceThreshold && distance < nearestDistance)
            {
                nearestPlanet = planet;
                nearestDistance = distance;
            }
        }

        return nearestPlanet;
    }

    void Launch(GameObject targetPlanet)
    {
        // Calculate the direction vector to the target planet
        Vector2 direction = (targetPlanet.transform.position - transform.position).normalized;

        // Apply the launch force
        rb.velocity = direction * launchForce;

        // Reset the launch timer
        launchTimerRemaining = launchTimer;

        // Set the launched flag
        isLaunched = true;
    }

    void ApplyGravityForce(GameObject targetPlanet)
    {
        // Calculate the direction vector to the target planet
        Vector2 direction = (targetPlanet.transform.position - transform.position).normalized;

        // Apply the gravity force
        rb.AddForce(direction * gravityForce, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with a planet
        if (collision.gameObject.tag == "Planet")
        {
            // Set the target planet
            targetPlanet = collision.gameObject;

            // Reset the launch timer
            launchTimerRemaining = launchTimer;

            // Set the launched flag
            isLaunched = false;
        }
    }

    void OnDrawGizmos()
    {
        // Find all planets
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        // Draw lines between planets
        foreach (GameObject planet1 in planets)
        {
            foreach (GameObject planet2 in planets)
            {
                if (planet1 != planet2)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(planet1.transform.position, planet2.transform.position);

                    // Calculate the distance between planets
                    float distance = Vector2.Distance(planet1.transform.position, planet2.transform.position);

                    // Draw the distance label
                    Gizmos.color = Color.yellow;
                    Vector3 labelPosition = (planet1.transform.position + planet2.transform.position) / 2;
                    labelPosition += new Vector3(0, 0.5f, 0); // offset the label slightly
                    Gizmos.DrawSphere(labelPosition, 0.1f);
                    UnityEditor.Handles.Label(labelPosition, distance.ToString("F2"));
                }
            }
        }
    }
}
