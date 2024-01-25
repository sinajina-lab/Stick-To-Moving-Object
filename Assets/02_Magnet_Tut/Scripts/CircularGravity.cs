using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularGravity : MonoBehaviour
{
    [SerializeField] float massOfEarth;
    [SerializeField] Transform centerOfEarth;
    [SerializeField] float G; //Gravity

    float massOfPlayer;
    float distance;
    float forceValue;
    Vector3 forceDirection;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        massOfPlayer = rb.mass;
        distance = Vector3.Distance(centerOfEarth.position, transform.position);
        forceValue = G * (massOfEarth * massOfPlayer) / (distance * distance);
    }

    // Update is called once per frame
    void Update()
    {
        forceDirection = (centerOfEarth.position - transform.position).normalized;
        rb.AddForce(forceValue * forceDirection); //We need value and direction of force
    }
}
