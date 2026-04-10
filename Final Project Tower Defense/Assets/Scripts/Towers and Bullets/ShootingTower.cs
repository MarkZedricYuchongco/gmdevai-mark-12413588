using UnityEngine;

public class ShootingTower : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float bulletSpeed = 20f;
    public float range = 10f;

    private float nextFireTime;
    private Transform target;

    void Update()
    {
        FindTarget();

        if (target != null)
        {
            Vector3 targetPos = PredictTargetPosition(target);
            transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));

            if (Time.time >= nextFireTime)
            {
                Shoot(targetPos);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= range)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        target = nearestEnemy?.transform;
    }

    Vector3 PredictTargetPosition(Transform target)
    {
        UnityEngine.AI.NavMeshAgent agent = target.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 enemyVelocity = agent != null ? agent.velocity : Vector3.zero;

        float distance = Vector3.Distance(firePoint.position, target.position);

        float travelTime = distance / bulletSpeed;

        return target.position + (enemyVelocity * travelTime);
    }

    void Shoot(Vector3 aimPoint)
    {
        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            Vector3 dir = (aimPoint - firePoint.position).normalized;
            bullet.Setup(dir, bulletSpeed);
        }
    }
}