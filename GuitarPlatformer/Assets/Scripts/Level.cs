using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    // attributes
    GameObject player;
    List<GameObject> obstacles;
    public GameObject obstaclePrefab;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        obstacles = new List<GameObject>();
        /*
        for (int i = 10; i < 150; i++)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, new Vector3(i, 5f, 0), transform.rotation);
            newObstacle.GetComponent<MeshRenderer>().material.color = Color.blue;
            obstacles.Add(newObstacle);
        }
        */
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
