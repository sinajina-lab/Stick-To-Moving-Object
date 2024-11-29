using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappling_user : MonoBehaviour
{
    private LineRenderer lineRend;
    private DistanceJoint2D distJoint;
    private Node selectedNode;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;

        distJoint = GetComponent<DistanceJoint2D>();
        distJoint.enabled = false;

        selectedNode = null;
    }

    // Update is called once per frame
    void Update()
    {
        NodeBehaviour();
    }

    public void SelectedNode(Node node )
    {
        selectedNode = node;
    }

    public void DeselectNode()
    {
        selectedNode = null;
    }

    private void NodeBehaviour()
    {
        if (selectedNode == null)
        {
            lineRend.enabled = false;
            distJoint.enabled = false;

            return;
        }

        lineRend.enabled = true;
        distJoint.enabled = true;

        distJoint.connectedBody = selectedNode.GetComponent<Rigidbody2D>();

        if(selectedNode != null)
        {
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, selectedNode.transform.position);
        }
    }
}
