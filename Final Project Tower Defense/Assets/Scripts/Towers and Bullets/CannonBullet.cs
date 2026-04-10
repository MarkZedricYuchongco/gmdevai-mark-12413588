using UnityEngine;

public class CannonBullet : Bullet
{
    [Header("Explosion Stats")]
    public float explosionRadius = 3f;
    public GameObject explosionPrefab;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.gameObject.layer == LayerMask.NameToLayer("MapFloor"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Debug.Log("Cannon Bullet Exploded!");

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyController enemy = hit.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}