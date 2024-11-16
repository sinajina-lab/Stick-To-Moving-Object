using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceJointController : MonoBehaviour
{
    private DistanceJoint2D distanceJoint;

    void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enableCollision = false; // Initially disable collision
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Candy Planet"))
        {
            // Attach to the platform
            PlayerAttachToPlatform attachScript = GetComponent<PlayerAttachToPlatform>();
            if (attachScript != null)
            {
                attachScript.AttachToPlatform(collision.gameObject);
            }
        }
    }
}
