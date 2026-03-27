using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToGoal : MonoBehaviour
{
    public Transform goal;
    float speed = 5;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 direction = goal.position - transform.position;

        transform.LookAt(goal);

        if (direction.magnitude > 1f)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        
    }

}
