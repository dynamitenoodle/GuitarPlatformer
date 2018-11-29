using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : Obstacle {

	// Use this for initialization
	void Start () {
        
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }
}
