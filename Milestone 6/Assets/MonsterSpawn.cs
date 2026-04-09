using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monster;
    public GameObject savior;
    GameObject[] agents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(monster, hit.point, monster.transform.rotation);
                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hit.point);
                }
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                Instantiate(savior, hit.point, savior.transform.rotation);
                foreach (GameObject a in agents)
                {
                    a.GetComponent<AIControl>().FlockToObstacle(hit.point);
                }
            }
        }
    }
}
