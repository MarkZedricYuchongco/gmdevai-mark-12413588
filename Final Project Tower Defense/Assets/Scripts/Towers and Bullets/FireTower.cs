using UnityEngine;

public class FireTower : MonoBehaviour
{
    public int burnDamage = 2;
    public int totalTicks = 5;
    public float tickInterval = 0.5f;
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
                enemy.ApplyBurn(burnDamage, totalTicks, tickInterval);
            }
        }
    }
}