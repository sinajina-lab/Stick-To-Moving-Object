using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook_2 : MonoBehaviour
{
    [SerializeField] LayerMask grappleLayer;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;
    [SerializeField] private LineRenderer rope;
    private Rigidbody2D rb;

    private bool isGrappling = false;
    private Vector2 target;
    private float currentRopeLength;
    private const float minRopeLength = 0.5f;
    private const float maxRopeLength = 10f;

    private RopeClimbing ropeClimbing;
    private Attractable_Player attractablePlayer;

    [SerializeField] private float autoGrappleRange = 5f;
    public Transform targetPlanet;
    [SerializeField] private float detectionRadius = 10f; // Set the radius for auto-detecting planets
    [SerializeField] private LayerMask planetLayer; // Layer assigned to planets

    private void Start()
    {
        ropeClimbing = GetComponent<RopeClimbing>();
        attractablePlayer = GetComponent<Attractable_Player>();

        rb = GetComponent<Rigidbody2D>();
    }

    private void AutoDetectPlanet()
    {
        Collider2D[] detectedPlanets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, planetLayer);
        if (detectedPlanets.Length > 0)
        {
            targetPlanet = detectedPlanets[0].transform; // Set the first detected planet as target
            Debug.Log("Auto-detected target planet: " + targetPlanet.name);
        }
    }

    private void Update()
    {
        // Check for auto-detection
        if (targetPlanet == null)
        {
            AutoDetectPlanet(); // Auto-detect target planet if not set
        }

        // Existing grapple logic
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StartGrapple();
        }

        if (isGrappling)
        {
            HandleGrappleMovement();
        }
    }

    private void Awake()
    {
        if (rope == null)
        {
            rope = GetComponent<LineRenderer>();
        }

        if (rope == null)
        {
            Debug.LogError("LineRenderer component is missing from the Player GameObject.");
        }
        else
        {
            Debug.Log("LineRenderer successfully assigned.");
            rope.enabled = false; // Ensure rope is initially disabled
        }
    }

    private void StartGrapple()
    {
        // Check for automatic grappling to the target planet
        if (targetPlanet != null && Vector2.Distance(transform.position, targetPlanet.position) <= autoGrappleRange)
        {
            Debug.Log("Automatic grapple to target planet: " + targetPlanet.name);
            isGrappling = true;
            target = targetPlanet.position; // Set target to the planet's position
            currentRopeLength = Vector2.Distance(transform.position, target);
            rope.enabled = true;
            rope.positionCount = 2;

            StartCoroutine(Grapple());
            attractablePlayer.SetGrappling(true); // Disable attraction
            return; // Exit the method to prevent manual grappling
        }

        // Manual grapple if no targetPlanet is within auto-grapple range
        //Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleLayer);
        //if (hit.collider != null)
        //{
        //    Debug.Log("Manual grapple hit: " + hit.collider.name);
        //    isGrappling = true;
        //    target = hit.point;
        //    currentRopeLength = Vector2.Distance(transform.position, target);
        //    rope.enabled = true;
        //    rope.positionCount = 2;

        //    StartCoroutine(Grapple());
        //    attractablePlayer.SetGrappling(true); // Disable attraction during grappling
        //}
        //else
        //{
        //    Debug.Log("No grapple hit detected.");
        //}
    }

    private IEnumerator Grapple()
    {
        float t = 0;
        float time = 1;

        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, transform.position);

        while (t < time)
        {
            Vector2 newPos = Vector2.Lerp(transform.position, target, t / time);
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, newPos);
            t += grappleShootSpeed * Time.deltaTime;
            yield return null;
        }

        rope.SetPosition(1, target);
        currentRopeLength = Vector2.Distance(transform.position, target);
        EnableClimbing(); // Enable climbing when grappling is complete
    }

    private void EnableClimbing()
    {
        if (ropeClimbing != null)
        {
            ropeClimbing.StartClimbing(currentRopeLength);
        }
    }

    private void HandleGrappleMovement()
    {
        currentRopeLength = Vector2.Distance(transform.position, target);
        if (currentRopeLength > maxRopeLength)
        {
            rope.SetPosition(0, transform.position);
            rope.SetPosition(1, target);
            EndGrapple();
        }
    }

    private void EndGrapple()
    {
        isGrappling = false;
        rope.enabled = false;
        attractablePlayer.SetGrappling(false); // Re-enable attraction
        if (ropeClimbing != null)
        {
            ropeClimbing.StopClimbing();
        }
    }

    public void UpdateRopeLength(float ropeLength)
    {
        currentRopeLength = ropeLength;
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, target - (Vector2.up * currentRopeLength));
    }
}
