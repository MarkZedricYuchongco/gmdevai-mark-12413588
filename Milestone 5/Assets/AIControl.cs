using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;

    public WASDMovement playerMovement;

    public enum ZombieType
    {
        Pursuer,
        Hider,
        Evader
    }

    public ZombieType zombieType;
    public float detectionRadius = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        playerMovement = target.GetComponent<WASDMovement>();
    }

    void Seek(Vector3 location)
    {
        agent.SetDestination(location);
    }

    void Flee(Vector3 location)
    {
        Vector3 fleeDirection = location - this.transform.position;
        agent.SetDestination(this.transform.position - fleeDirection);
    }

    void Pursue()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);

        Seek(target.transform.position + targetDirection * lookAhead);
    }

    void Evade()
    {
        Vector3 targetDirection = target.transform.position - this.transform.position;
        float lookAhead = targetDirection.magnitude / (agent.speed + playerMovement.currentSpeed);
        Flee(target.transform.position + target.transform.forward * lookAhead);
    }

    Vector3 wanderTarget;

    void Wander()
    {
        float wanderRadius = 20;
        float wanderDistance = 10;
        float wanderJitter = 1;

        wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter,
                                    0,
                                    Random.Range(-1f, 1f) * wanderJitter);
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

        Seek(targetWorld);
    }

    void Hide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        Vector3 chosenDir = Vector3.zero;
        GameObject chosenGameObject = World.Instance.GetHidingSpots()[0];

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                chosenDir = hideDirection;
                chosenGameObject = World.Instance.GetHidingSpots()[i];
                distance = spotDistance;
            }
        }

        Collider hideCol = chosenGameObject.GetComponent<Collider>();
        Ray back = new Ray(chosenSpot, -chosenDir.normalized);
        RaycastHit info;
        float rayDistance = 100f;
        hideCol.Raycast(back, out info, rayDistance);

        Seek(info.point + chosenDir.normalized * 5);
    }

    void CleverHide()
    {
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;

        int hidingSpotsCount = World.Instance.GetHidingSpots().Length;
        for (int i = 0; i < hidingSpotsCount; i++)
        {
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * 5;

            float spotDistance = Vector3.Distance(this.transform.position, hidePosition);
            if (spotDistance < distance)
            {
                chosenSpot = hidePosition;
                distance = spotDistance;
            }
        }

        Seek(chosenSpot);
    }

    bool canSeeTarget()
    {
        RaycastHit raycastInfo;
        Vector3 rayToTarget = target.transform.position - this.transform.position;

        Vector3 startPos = this.transform.position + Vector3.up * 1.5f; // AI Eye level
        Vector3 endPos = target.transform.position + Vector3.up * 1.0f; // Player Chest level
        Vector3 direction = endPos - startPos;

        Debug.DrawRay(startPos, direction, Color.red);

        if (Physics.Raycast(startPos, direction, out raycastInfo))
        {
            return raycastInfo.transform.CompareTag("Player");
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float distToTarget = Vector3.Distance(this.transform.position, target.transform.position);

        if (distToTarget < detectionRadius && canSeeTarget() == true)
        {
            switch (zombieType)
            {
                case ZombieType.Pursuer:
                    Pursue();
                    break;
                case ZombieType.Hider:
                    Hide();
                    break;
                case ZombieType.Evader:
                    Evade();
                    break;
            }
        }
        else
        {
            Wander();
        }
    }
}
