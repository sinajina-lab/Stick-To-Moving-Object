using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachToPlatform : MonoBehaviour
{
    private DistanceJoint2D distanceJoint;

    void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
    }

    public void AttachToPlatform(GameObject platform)
    {
        Rigidbody2D platformRigidbody = platform.GetComponent<Rigidbody2D>();
        if (platformRigidbody != null)
        {
            distanceJoint.connectedBody = platformRigidbody;

            // Set the connected anchor to the center of the platform
            Vector2 platformCenter = platform.GetComponent<Collider2D>().bounds.center;
            distanceJoint.connectedAnchor = platformCenter;

            // Enable collision
            distanceJoint.enableCollision = true;
        }
    }

    public void DetachFromPlatform()
    {
        distanceJoint.connectedBody = null;
        distanceJoint.enableCollision = false;
    }
}
