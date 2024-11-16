using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLaunch : MonoBehaviour
{
    public float launchPower = 1000f;
    public Transform launchDirection;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Launch when spacebar is pressed
        {
            Launch();
        }
    }

    private void Launch()
    {
        Vector2 launchDir = (launchDirection.position - transform.position).normalized;
        rb.AddForce(launchDir * launchPower);
    }
}
