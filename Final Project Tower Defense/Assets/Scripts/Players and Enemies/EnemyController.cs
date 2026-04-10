using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    GameObject goal;
    NavMeshAgent agent;

    [Header("Stats")]
    public int health = 30;
    public int damageToBase = 1;
    public int goldReward = 10;

    private Coroutine currentBurnRoutine;
    private bool isBurning = false;

    [Header("Movement")]
    public float baseSpeed = 3.5f;
    private float slowTimer = 0f;
    private bool isSlowed = false;

    float arrivalThreshold = 1f;

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Goal");
        agent = GetComponent<NavMeshAgent>();

        agent.speed = baseSpeed;
        agent.SetDestination(goal.transform.position);
        agent.stoppingDistance = 0f;
    }

    void Update()
    {
        HandleSlowTimer();

        float distanceToGoal = Vector3.Distance(transform.position, goal.transform.position);
        if (distanceToGoal <= arrivalThreshold)
        {
            ReachedGoal();
        }
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Die();
    }

    void Die()
    {
        GameManager.Instance.gold += goldReward; 
        Destroy(gameObject);
    }

    public void ApplyBurn(int damage, int ticks, float interval)
    {
        if (isBurning) return;

        currentBurnRoutine = StartCoroutine(BurnRoutine(damage, ticks, interval));
    }

    IEnumerator BurnRoutine(int damage, int ticks, float interval)
    {
        isBurning = true;

        for (int i = 0; i < ticks; i++)
        {
            yield return new WaitForSeconds(interval);
            TakeDamage(damage);

            Debug.Log($"{gameObject.name} took {damage} burn damage.");
        }

        isBurning = false;
        currentBurnRoutine = null;
    }

    public void ApplySlow(float multiplier, float duration)
    {
        agent.speed = baseSpeed * multiplier;
        slowTimer = duration;
        isSlowed = true;
    }

    void HandleSlowTimer()
    {
        if (isSlowed)
        {
            slowTimer -= Time.deltaTime;
            if (slowTimer <= 0)
            {
                agent.speed = baseSpeed;
                isSlowed = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal")) ReachedGoal();
    }

    void ReachedGoal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage(damageToBase);
        }
        Destroy(gameObject);
    }
}