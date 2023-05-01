using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public ParticleSystem hitParticles;
    public float lifetime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ParticleSystem particles = Instantiate(hitParticles, collision.contacts[0].point, Quaternion.identity);
        particles.Play();
        Destroy(gameObject);
        Destroy(hitParticles, 2f);
    }
}