using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PointEffector2D explosionComponent;
    [SerializeField] CircleCollider2D circleCollider;

    [SerializeField] GameObject debrisParticles;

    [SerializeField] float addTorqueAmountInDegrees;
    [SerializeField] float explosionDelay = 3f; // Time before explosion in seconds

    private float explosionRadius;
    private bool isExploding = false;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        explosionComponent = GetComponent<PointEffector2D>();

        // Disable explosion component initially
        explosionComponent.enabled = false;
        explosionRadius = circleCollider.radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (isExploding)
        {
            explosionDelay -= Time.deltaTime;
            if (explosionDelay <= 0)
            {
                TriggerExplosion();
                isExploding = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isExploding = true;
        }
    }

    private void TriggerExplosion()
    {
        explosionComponent.enabled = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody2D rigid = colliders[i].GetComponentInParent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.AddTorque(addTorqueAmountInDegrees * Mathf.Deg2Rad * rigid.inertia);
            }
        }

        // Instantiate and play the particle system
        GameObject particles = Instantiate(debrisParticles, transform.position, Quaternion.identity);

        // Optional: Destroy the particle GameObject after it finishes
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(particles, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
        }

        // Destroy the bomb after playing particles
        Destroy(this.gameObject);
    }
}
