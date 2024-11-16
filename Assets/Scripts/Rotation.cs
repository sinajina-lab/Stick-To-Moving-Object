using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Rotational Speed
    [SerializeField] float speed = 0f;

    // Forward Direction
    [SerializeField] bool ForwardX = false;
    [SerializeField] bool ForwardY = false;
    [SerializeField] bool ForwardZ = false;

    // Reverse Direction
    [SerializeField] bool ReverseX = false;
    [SerializeField] bool ReverseY = false;
    [SerializeField] bool ReverseZ = false;

    void Update()
    {
        // Check if the screen is being tapped and held
        //if (Input.GetMouseButton(0))
        //{
        //    // Stop rotation when holding
        //    return;
        //}

        // Forward Direction
        if (ForwardX)
        {
            transform.Rotate(Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if (ForwardY)
        {
            transform.Rotate(0, Time.deltaTime * speed, 0, Space.Self);
        }
        if (ForwardZ)
        {
            transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
        }

        // Reverse Direction
        if (ReverseX)
        {
            transform.Rotate(-Time.deltaTime * speed, 0, 0, Space.Self);
        }
        if (ReverseY)
        {
            transform.Rotate(0, -Time.deltaTime * speed, 0, Space.Self);
        }
        if (ReverseZ)
        {
            transform.Rotate(0, 0, -Time.deltaTime * speed, Space.Self);
        }
    }
}
