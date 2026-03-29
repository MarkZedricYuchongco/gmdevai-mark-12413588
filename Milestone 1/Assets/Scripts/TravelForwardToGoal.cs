using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelForwardToGoal : MonoBehaviour
{
    public Transform goal;

    public float maxSpeed = 10;
    public float accel = 5;
    public float decel = 9;

    private float currentSpeed = 0;

    public float rotSpeed = 4;

    public float distanceToGoal = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goal.position.x, transform.position.y, goal.position.z);
        Vector3 targetDirection = (lookAtGoal - transform.position).normalized;

        if (targetDirection != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, targetDirection, rotSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(lookAtGoal, transform.position) > distanceToGoal)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, accel * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, decel * Time.deltaTime);
        }

        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        /*
        Vector3 lookAtGoal = new Vector3(goal.position.x,
                                         this.transform.position.y,
                                         goal.position.z);

        Vector3 direction = lookAtGoal - transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                 Quaternion.LookRotation(direction),
                                                 rotSpeed * Time.deltaTime);

        if (Vector3.Distance(lookAtGoal, transform.position) > distanceToGoal)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        */

    }

}
