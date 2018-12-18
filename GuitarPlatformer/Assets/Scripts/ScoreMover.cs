using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMover : MonoBehaviour {

    public float freq;
    public float amp; 

    private RectTransform text_position;
    private UnityEngine.UI.Text text;
    private int score;
    private int incr;

	void Start () {
        text_position = this.GetComponent<RectTransform>();
        text = this.GetComponent<UnityEngine.UI.Text>();
	}

    public void ResetScore()
    {
        score = 0;
    }
	
	void Update () {
        incr++;
		if(text_position != null)
        {
            float new_y = text_position.position.y + Time.deltaTime * amp * Mathf.Sin(incr * freq);
            text_position.position = new Vector3(text_position.position.x, new_y, text_position.position.z);
            text_position.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sin(incr * freq)));
        }

        if(text != null)
        {
            if(incr % 20 == 0)
            {
                score++;
            }

            text.text = "Score: " + score;
        }
	}
}
