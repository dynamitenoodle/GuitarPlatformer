﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    // attributes
    protected GameObject player;
    float deleteDistance = 15f;
    public float bpm;
    Vector3 vel;
    public Vector3 direction;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        vel = new Vector3();
	}
	
	// Update is called once per frame
	void Update ()
    {
        vel = direction * bpm / 60 * Time.deltaTime * 400;
        GetComponent<Rigidbody2D>().velocity = vel;

        if (DestroyCheck())
        {
            Destroy(gameObject);
        }
	}

    bool DestroyCheck(){ if (transform.position.x < -deleteDistance) { return true; } return false;}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player.GetComponent<Player>().TakeDamage();

    }
}
