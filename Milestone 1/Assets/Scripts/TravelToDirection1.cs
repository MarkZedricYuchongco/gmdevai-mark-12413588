using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelToDirection1 : MonoBehaviour {

    public Vector3 direction = new Vector3(8, 0, 4);
    float movementSpeed = 5;

    private void Start()
    {
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime);
    }
}
