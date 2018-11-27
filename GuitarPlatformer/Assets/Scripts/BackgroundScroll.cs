using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    Sprite bg;
    bool createdChild;
	public float scrollSpeed = .1f, offScreen = 20f;

	// Use this for initialization
	void Start () {
        bg = GetComponent<SpriteRenderer>().sprite;
        createdChild = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position -= new Vector3(scrollSpeed, 0, 0);

        if (transform.position.x <= offScreen && !createdChild)
        {
            GameObject newBg = Instantiate(gameObject, transform);
            newBg.transform.parent = null;
            newBg.transform.position = new Vector3(transform.position.x + bg.bounds.extents.x * 2, transform.position.y, 0);
            newBg.transform.localScale = transform.localScale;
            newBg.name = gameObject.name;
            createdChild = true;
        }

        else if (transform.position.x <= -offScreen)
            Destroy(gameObject);
	}
}
