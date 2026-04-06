using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    Transform goal;
    float speed = 5;
    float accuracy = 1;
    float rotSpeed = 2;
    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWaypointIndex = 0;
    Graph graph;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wps = wpManager.GetComponent<WaypointManager>().waypoints;
        graph = wpManager.GetComponent<WaypointManager>().graph;
        currentNode = wps[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (graph.getPathLength() == 0 || currentWaypointIndex == graph.getPathLength())
        {
            return;
        }

        // the node we are closest to at the moment
        currentNode = graph.getPathPoint(currentWaypointIndex);

        // if we are close enough to the current node, move to the next one
        if (Vector3.Distance(transform.position, currentNode.transform.position) < accuracy)
        {
            currentWaypointIndex++;
        }

        // if we are not at the end of the path
        if (currentWaypointIndex < graph.getPathLength())
        {
            goal = graph.getPathPoint(currentWaypointIndex).transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                       Quaternion.LookRotation(direction),
                                                       Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }

    public void GoToHelipad()
    {
        this.graph.AStar(currentNode, wps[0]);
        this.currentWaypointIndex = 0;
    }

    public void GoToRuins()
    {
        this.graph.AStar(currentNode, wps[6]);
        this.currentWaypointIndex = 0;
    }

    public void GoToFactory()
    {
        this.graph.AStar(currentNode, wps[7]);
        this.currentWaypointIndex = 0;
    }

    public void GoToTwinMountains()
    {
        this.graph.AStar(currentNode, wps[2]);
        this.currentWaypointIndex = 0;
    }

    public void GoToBarracks()
    {
        this.graph.AStar(currentNode, wps[17]);
        this.currentWaypointIndex = 0;
    }

    public void GoToCommandCenter()
    {
        this.graph.AStar(currentNode, wps[16]);
        this.currentWaypointIndex = 0;
    }

    public void GoToOilPumps()
    {
        this.graph.AStar(currentNode, wps[14]);
        this.currentWaypointIndex = 0;
    }

    public void GoToTankers()
    {
        this.graph.AStar(currentNode, wps[15]);
        this.currentWaypointIndex = 0;
    }

    public void GoToRadar()
    {
        this.graph.AStar(currentNode, wps[11]);
        this.currentWaypointIndex = 0;
    }

    public void GoToCommandPost()
    {
        this.graph.AStar(currentNode, wps[12]);
        this.currentWaypointIndex = 0;
    }

    public void GoToMiddle()
    {
        this.graph.AStar(currentNode, wps[13]);
        this.currentWaypointIndex = 0;
    }
}
