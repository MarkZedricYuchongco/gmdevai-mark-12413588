using UnityEngine;

public class SlowTower : MonoBehaviour
{
    public float slowAmount = 0.5f;
    public float slowDuration = 1.0f;
    public float range = 5f;

    void Start()
    {
        BoxCollider col = gameObject.GetComponent<BoxCollider>();
        if (col == null) col = gameObject.AddComponent<BoxCollider>();
        col.isTrigger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount, slowDuration);
            }
        }
    }
}