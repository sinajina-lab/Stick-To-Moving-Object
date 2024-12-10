using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private grappling_user grappling_userScript;

    void Start()
    {
        grappling_userScript = GameObject.FindGameObjectWithTag("Player").GetComponent<grappling_user>();
    }

    public void OnMouseDown()
    {
        Collider2D[] nearbyNodes = Physics2D.OverlapCircleAll(transform.position, 1f);
        Node closestNode = this;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D col in nearbyNodes)
        {
            Node potentialNode = col.GetComponent<Node>();
            if (potentialNode != null && Vector3.Distance(transform.position, col.transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(transform.position, col.transform.position);
                closestNode = potentialNode;
            }
        }

        grappling_userScript.SelectedNode(closestNode);
    }

    public void OnMouseUp()
    {
        grappling_userScript.DeselectNode();
    }
}
