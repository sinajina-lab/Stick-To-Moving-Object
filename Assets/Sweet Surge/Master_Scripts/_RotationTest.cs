using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _RotationTest : MonoBehaviour
{
    public float speed = 0f;
    public Vector3 rotationAxis = Vector3.forward;

    public void Update()
    {
        transform.Rotate(rotationAxis * speed * Time.deltaTime);
    }
}
