using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private GameObject player;
    private grappling_user grappling_userScript;
    private Node node;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        node = null;
        grappling_userScript = player.GetComponent<grappling_user>();
    }

    public void OnMouseDown()
    {
        node = this;
        grappling_userScript.SelectedNode(node);
    }

    public void OnMouseUp()
    {
        node = null;
        grappling_userScript.DeselectNode();
    }
}
