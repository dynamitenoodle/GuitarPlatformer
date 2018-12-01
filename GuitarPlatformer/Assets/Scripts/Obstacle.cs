using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    // attributes
    GameObject player;
    float deleteDistance = 10f, speed = .1f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position -= new Vector3(speed, 0, 0);

        if (DestroyCheck())
        {
            Destroy(gameObject);
        }
	}

    bool DestroyCheck(){ if (transform.position.x < deleteDistance) { return true; } return false;}
}
