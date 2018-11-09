using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    // attributes
    GameObject player;
    public float speed = 4.0f;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        GetComponent<SpriteRenderer>().color = GetRandomColor();
    }

    // Update is called once per frame
    void Update () {
		if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (Vector2.Distance(transform.position, player.transform.position) > 25f)
            Destroy(gameObject);
    }

    // Gets a random color
    Color GetRandomColor() { return new Color(Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f, Random.Range(0, 255) / 255.0f); }
}
