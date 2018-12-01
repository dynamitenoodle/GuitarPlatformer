using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note : MonoBehaviour {
    public float length;
    public float bpm;
    Vector3 vel;
    public Vector3 direction;
	// Use this for initialization
	void Start () {
        vel = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(length, 1.3f, 1);
        vel = direction*bpm/60*Time.deltaTime*500;
        GetComponent<Rigidbody2D>().velocity = vel;
        if (!GetComponent<MeshRenderer>().isVisible)
        {
            Destroy(this.gameObject);
        }
	}
}
