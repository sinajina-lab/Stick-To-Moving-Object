using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangularGravity : MonoBehaviour
{
    [SerializeField] private GameObject rectangularObject;
    [SerializeField] private float massOfRectangle;
    [SerializeField] private float G; // Gravity constant

    private Rigidbody2D rb;
    private float massOfPlayer;
    private Vector2 forceDirection;
    private float forceValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        massOfPlayer = rb.mass;
    }

    void Update()
    {
        Vector2 closestPoint = GetClosestPointOnRectangle(rectangularObject.GetComponent<Collider2D>());
        Vector2 directionToClosestPoint = closestPoint - (Vector2)transform.position;
        float distance = directionToClosestPoint.magnitude;

        // Ensure we do not divide by zero in case the player is exactly on the closest point
        if (distance == 0f)
            return;

        forceDirection = directionToClosestPoint.normalized;
        forceValue = G * (massOfRectangle * massOfPlayer) / (distance * distance);

        rb.AddForce(forceValue * forceDirection);
    }

    Vector2 GetClosestPointOnRectangle(Collider2D collider)
    {
        return collider.ClosestPoint(transform.position);
    }
}
