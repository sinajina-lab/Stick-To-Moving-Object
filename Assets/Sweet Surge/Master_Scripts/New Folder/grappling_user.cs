using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappling_user : MonoBehaviour
{
    private LineRenderer lineRend;
    private DistanceJoint2D distJoint;
    private Node selectedNode;

    [SerializeField] private int resolution = 50;
    [SerializeField] private float waveSize = 1f, animSpeed = 5f;

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;

        distJoint = GetComponent<DistanceJoint2D>();
        distJoint.enabled = false;

        selectedNode = null;
    }

    private void OnEnable()
    {
        Bomb.OnBombDestroyed += HandleBombDestroyed;
    }

    private void OnDisable()
    {
        Bomb.OnBombDestroyed -= HandleBombDestroyed;
    }

    void Update()
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

        lineRend.SetPosition(0, transform.position);
        lineRend.SetPosition(1, selectedNode.transform.position);
    }

    public void SelectedNode(Node node)
    {
        selectedNode = node;
        if (selectedNode != null)
        {
            StartCoroutine(AnimateRope(selectedNode.transform.position));
        }
    }

    public void DeselectNode()
    {
        selectedNode = null;
    }

    private IEnumerator AnimateRope(Vector3 targetPos)
    {
        lineRend.positionCount = resolution;
        float percent = 0f;

        while (percent <= 1f)
        {
            percent += Time.deltaTime * animSpeed;
            SetPoints(targetPos, percent);
            yield return null;
        }

        SetPoints(targetPos, 1f);
    }

    private void SetPoints(Vector3 targetPos, float percent)
    {
        Vector3 ropeEnd = Vector3.Lerp(transform.position, targetPos, percent);
        float length = Vector3.Distance(transform.position, ropeEnd);

        for (int i = 0; i < resolution; i++)
        {
            float xPos = (float)i / resolution * length;
            float yPos = Mathf.Sin(i * waveSize * Mathf.PI * 2 / resolution) * (1f - percent);
            lineRend.SetPosition(i, new Vector3(xPos + transform.position.x, yPos + transform.position.y, 0));
        }
    }

    private void HandleBombDestroyed(Vector3 position)
    {
        if (selectedNode != null && Vector3.Distance(selectedNode.transform.position, position) < 0.1f)
        {
            DeselectNode();
        }
    }
}
