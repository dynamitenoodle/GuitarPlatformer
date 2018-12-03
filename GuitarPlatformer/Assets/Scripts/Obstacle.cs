using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour {

    // attributes
    protected GameObject player;
    float deleteDistance = 15f;
    float bpm;
    Vector3 vel;
    [HideInInspector]
    public Vector3 direction;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        vel = new Vector3();
        bpm = player.GetComponent<Player>().bpm;
        if (direction == Vector3.zero)
            direction = new Vector3(-1, 0, 0);
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (SceneManager.GetActiveScene().name == "LevelBuild")
            transform.position -= new Vector3(.1f, 0, 0);

        else
        {
            vel = direction * bpm / 60 * Time.deltaTime * 400;
            GetComponent<Rigidbody2D>().velocity = vel;
        }

        if (DestroyCheck())
            Destroy(gameObject);
	}

    bool DestroyCheck(){ if (transform.position.x < -deleteDistance) { return true; } return false;}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player.GetComponent<Player>().TakeDamage();

    }
}
