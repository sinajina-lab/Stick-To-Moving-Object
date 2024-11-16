using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable_Player : MonoBehaviour
{
    [SerializeField] private bool rotateToCenter = true;
    [SerializeField] private Planet_Attractor currentAttractor;
    [SerializeField] private float gravityStrength = 100;
    [SerializeField] public bool isClimbing = false;
    [SerializeField] public bool isGrappling = false;

    Transform m_transform;
    Collider2D m_collider;
    Rigidbody2D m_rigidbody;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (currentAttractor != null && !isClimbing && !isGrappling)
        {
            if (!currentAttractor.AttractedObjects.Contains(m_collider))
            {
                currentAttractor = null;
                return;
            }
            if (rotateToCenter) RotateToCenter();
            m_rigidbody.gravityScale = 0; // Disable regular gravity
        }
        else
        {
            m_rigidbody.gravityScale = 1; // Re-enable gravity if not on a planet
        }
    }

    public void Attract(Planet_Attractor attractorObj)
    {
        if (isClimbing || isGrappling) return; // Disable attraction when climbing or grappling

        Vector2 attractionDir = ((Vector2)attractorObj.attractorTransform.position - m_rigidbody.position).normalized;
        m_rigidbody.AddForce(attractionDir * attractorObj.gravity * gravityStrength * Time.fixedDeltaTime);

        if (currentAttractor == null)
        {
            currentAttractor = attractorObj;
        }
    }

    public void SetClimbing(bool climbing)
    {
        isClimbing = climbing;
        if (climbing)
        {
            m_rigidbody.gravityScale = 0; // Disable gravity while climbing
        }
    }

    public void SetGrappling(bool grappling)
    {
        isGrappling = grappling;
        if (grappling)
        {
            m_rigidbody.gravityScale = 0; // Disable gravity while grappling
        }
    }

    void RotateToCenter()
    {
        if (currentAttractor != null)
        {
            Vector2 distanceVector = (Vector2)currentAttractor.attractorTransform.position - (Vector2)m_transform.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }
}
