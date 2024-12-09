using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappling_user : MonoBehaviour
{
    private LineRenderer lineRend;
    private DistanceJoint2D distJoint;
    private Node selectedNode;

    [SerializeField] int resolution, waveCount, wobbleCount;
    [SerializeField] float waveSize, animSpeed;
    [SerializeField] LayerMask nodeLayer; // LayerMask for nodes
    [SerializeField] float maxGrappleDistance = 10f;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;

        distJoint = GetComponent<DistanceJoint2D>();
        distJoint.enabled = false;

        selectedNode = null;
    }

    void Update()
    {
        HandleScreenTap();
        NodeBehaviour();
    }

    public void SelectNode(Node node)
    {
        if (node == null) return;

        selectedNode = node;
        StartCoroutine(AnimateRope(selectedNode.transform.position));
    }

    public void DeselectNode()
    {
        selectedNode = null;

        // Disable the DistanceJoint2D and LineRenderer
        distJoint.enabled = false;
        lineRend.enabled = false;

        // Reset LineRenderer points to clear the rope
        lineRend.positionCount = 0;
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

        if (selectedNode != null)
        {
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, selectedNode.transform.position);
        }
    }

    private void HandleScreenTap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;

            Node nearestNode = FindNearestNode(worldPos);

            if (nearestNode != null)
            {
                SelectNode(nearestNode);
            }
        }
    }

    private Node FindNearestNode(Vector3 position)
    {
        Collider2D[] nodesInRange = Physics2D.OverlapCircleAll(position, maxGrappleDistance, nodeLayer);
        Node nearestNode = null;
        float minDistance = float.MaxValue;

        foreach (var collider in nodesInRange)
        {
            Node node = collider.GetComponent<Node>();
            if (node != null)
            {
                float distance = Vector2.Distance(position, node.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestNode = node;
                }
            }
        }
        return nearestNode;
    }

    private IEnumerator AnimateRope(Vector3 targetPos)
    {
        lineRend.positionCount = resolution;
        float angle = LookAtAngle(targetPos - transform.position);

        float percent = 0;
        while (percent <= 1f)
        {
            percent += Time.deltaTime * animSpeed;
            SetPoints(targetPos, percent, angle);
            yield return null;
        }
        SetPoints(targetPos, 1, angle);
    }

    private void SetPoints(Vector3 targetPos, float percent, float angle)
    {
        Vector3 ropeEnd = Vector3.Lerp(transform.position, targetPos, percent);
        float length = Vector2.Distance(transform.position, ropeEnd);

        for (int i = 0; i < resolution; i++)
        {
            float xPos = (float)i / resolution * length;
            float reversePercent = (1 - percent);

            float amplitude = Mathf.Sin(reversePercent * wobbleCount * Mathf.PI) * ((1f - (float)i / resolution) * waveSize);

            float yPos = Mathf.Sin((float)waveCount * i / resolution * 2 * Mathf.PI * reversePercent) * amplitude;

            Vector2 pos = RotatePoint(new Vector2(xPos + transform.position.x, yPos + transform.position.y), transform.position, angle);
            lineRend.SetPosition(i, pos);
        }
    }

    Vector2 RotatePoint(Vector2 point, Vector2 pivot, float angle)
    {
        Vector2 dir = point - pivot;
        dir = Quaternion.Euler(0, 0, angle) * dir;
        return dir + pivot;
    }

    private float LookAtAngle(Vector2 target)
    {
        return Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
    }
}
