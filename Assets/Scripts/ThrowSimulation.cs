using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSimulation : MonoBehaviour
{
    public GameObject Arrow;
    public float LaunchForce;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        GameObject ArrowIns = Instantiate(Arrow, transform.position, transform.rotation);
        ArrowIns.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce);
    }
}
