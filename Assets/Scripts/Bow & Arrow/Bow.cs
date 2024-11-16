using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject player;  // player game object
    public float launchForce;  // launch force
    public Transform shotPoint; // shot point position when player lands on the ground
    public GameObject point; //projectile
    GameObject[] points;
    public int numberOfPoints;  // control number of points that you want
    public float spaceBetweenPoints; // allows to tweak how much space between each points
    Vector2 direction;
    bool isGrounded;
    public Transform targetPlanet; // The next planet to move towards

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlanet != null)
        {
            direction = targetPlanet.position - player.transform.position; // Calculate direction towards the target planet
            transform.right = direction;  // Rotate the bow towards the target

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * launchForce;  // Apply force in the direction of the target planet
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }
}
