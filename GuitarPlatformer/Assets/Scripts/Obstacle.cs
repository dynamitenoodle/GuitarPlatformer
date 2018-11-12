using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    public bool destructable;
    public GameObject player;

	// Use this for initialization
	void Start () {
        destructable = false;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        CheckCollision();
        DestructableCollision();
        Cleanup();
	}

    //Shift the obstacle with the background
    void Move()
    {
        //constantly sliding left until cleanup
    }

    //Delete obstacle upon leaving the screen
    void Cleanup()
    {
        //delete at x -7.3
    }

    //Collision Check
    void CheckCollision()
    {
        //this will kill the player if they aren't shielded
    }

    //Collisions if Destructable
    void DestructableCollision()
    {
        if (destructable)
        {
            //check collisions with shielded or projectile

        }
    }
}
