using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void BombDestroyedEvent(Vector3 position);
    public static event BombDestroyedEvent OnBombDestroyed;

    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private GameObject debrisParticles;
    [SerializeField] private float addTorqueAmountInDegrees = 200f;
    [SerializeField] private float explosionDelay = 3f;

    private PointEffector2D explosionComponent;
    private float explosionRadius;
    private bool isExploding = false;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        explosionComponent = GetComponent<PointEffector2D>();

        explosionComponent.enabled = false;
        explosionRadius = circleCollider.radius;
    }

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
        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rigid = col.GetComponentInParent<Rigidbody2D>();
            if (rigid != null)
            {
                rigid.AddTorque(addTorqueAmountInDegrees * Mathf.Deg2Rad * rigid.inertia);
            }
        }

        OnBombDestroyed?.Invoke(transform.position);

        GameObject particles = Instantiate(debrisParticles, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            Destroy(particles, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
        }

        Destroy(this.gameObject);
    }
}
