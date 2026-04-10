using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Vector3 direction;
    protected float speed;
    public int damage = 10;

    public virtual void Setup(Vector3 dir, float moveSpeed)
    {
        direction = dir;
        speed = moveSpeed;
        Destroy(gameObject, 5f);
    }

    protected virtual void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}