using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Planet_Attractor : MonoBehaviour
{
    public LayerMask AttractionLayer;
    public float gravity = 10;
    [SerializeField] private float radius = 10;
    public List<Collider2D> AttractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform attractorTransform;

    void Awake()
    {
        attractorTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetAttractedObjects();
    }

    void FixedUpdate()
    {
        AttractObjects();
    }

    void SetAttractedObjects()
    {
        // Check for objects within radius on the specified layer
        AttractedObjects = Physics2D.OverlapCircleAll(attractorTransform.position, radius, AttractionLayer).ToList();
        Debug.Log("Attracted objects count: " + AttractedObjects.Count);
    }

    void AttractObjects()
    {
        foreach (var obj in AttractedObjects)
        {
            var player = obj.GetComponent<Attractable_Player>();
            if (player != null && !player.isGrappling && !player.isClimbing)
            {
                player.Attract(this);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
