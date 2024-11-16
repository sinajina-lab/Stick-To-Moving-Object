using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private Vector2 CannonPosition;
    private Vector2 direction;

    public float FireForce = 10f; // Set a default fire force
    public Transform FirePoint; // FirePoint is a child of the player
    private bool isGrounded = false; // Check if the player is on the platform

    public GameObject point; // Visual representation of the projectile path
    GameObject[] points;
    public int numberOfPoints = 10; // Control the number of points
    public float spaceBetweenPoints = 0.2f; // Space between each point

    // Start is called before the first frame update
    void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, FirePoint.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the position of the trajectory points
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }

        // Fire the player when the left mouse button is clicked and the player is grounded
        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Calculate the direction based on the FirePoint
        direction = FirePoint.up; // You may need to adjust the direction based on your setup

        // Apply force to the player's Rigidbody2D
        Rigidbody2D playerRigidbody = FirePoint.GetComponentInParent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(direction * FireForce, ForceMode2D.Impulse);
            isGrounded = false; // Player is no longer on the cannon
        }
    }

    // Calculate the position of each point along the trajectory
    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)FirePoint.position + (direction.normalized * FireForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

    // Trigger detection to check if the player is on top of the cannon
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet")) // Assuming the feet collider is tagged as "PlayerFeet"
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerFeet"))
        {
            isGrounded = false;
        }
    }
}
