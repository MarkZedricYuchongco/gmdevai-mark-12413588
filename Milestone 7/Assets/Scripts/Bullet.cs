using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	
	void OnCollisionEnter(Collision col)
    {
    	GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);

        if (col.gameObject.CompareTag("player"))
        {
            Debug.Log("I hit the Player!");
        }
        if (col.gameObject.CompareTag("enemy"))
        {
            Debug.Log("I hit an Enemy!");
        }

        if (!col.gameObject.CompareTag("bullet"))
        {
            Destroy(e, 1.5f);
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
