using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject player;
    private grappling_user grappling_userScript;

    void Start()
    {
        // Find the player and its grappling script
        player = GameObject.FindGameObjectWithTag("Player");
        grappling_userScript = player.GetComponent<grappling_user>();
    }

    private void OnMouseDown()
    {
        // When the user clicks on this node, select it
        grappling_userScript.SelectNode(this);
    }

    private void OnMouseUp()
    {
        // When the user releases the mouse, deselect the node
        grappling_userScript.DeselectNode();
    }
}
