using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AgentManager : MonoBehaviour
{
    GameObject[] agents;
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    Vector2 mousePosition = Mouse.current.position.ReadValue();

        //    RaycastHit hit;

        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition), out hit))
        //    {
        //        foreach (GameObject ai in agents)
        //        {
        //            ai.GetComponent<AIControl>().agent.SetDestination(hit.point);
        //        }
        //    }
        //}

        foreach (GameObject ai in agents)
        {
            ai.GetComponent<AIControl>().agent.SetDestination(player.position);
        }
    }
}
