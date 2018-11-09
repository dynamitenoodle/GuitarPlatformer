using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    Sprite bg;
    bool createdChild;

	// Use this for initialization
	void Start () {
        bg = GetComponent<SpriteRenderer>().sprite;
        createdChild = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position -= new Vector3(.05f, 0, 0);

        if (transform.position.x <= -.5f && !createdChild)
        {
            GameObject newBg = Instantiate(gameObject, transform);
            newBg.transform.parent = null;
            newBg.transform.position = new Vector3(18f, 0, 0);
            newBg.transform.localScale = transform.localScale;
            newBg.name = gameObject.name;
            createdChild = true;
        }

        else if (transform.position.x <= -15f)
            Destroy(gameObject);
	}
}
