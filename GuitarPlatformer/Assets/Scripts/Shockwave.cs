using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

    // attributes
    GameObject player;
    Sprite curSprite;
    public List<Sprite> spriteSheet;
    int spriteNum;
    int spriteTimer;
    public int spriteTimerMax = 5;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteNum = -1;
        curSprite = GetComponent<SpriteRenderer>().sprite;
        spriteTimer = 0;
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        foreach (GameObject ground in GameObject.FindGameObjectsWithTag("Ground"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ground.GetComponent<Collider2D>());
        }
	}

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (spriteTimer % spriteTimerMax == 0)
        {
            if (spriteNum + 1 == spriteSheet.Count)
                Destroy(gameObject);

            else
            {
                curSprite = spriteSheet[spriteNum + 1];
                spriteNum++;
            }

            if (spriteNum == 4)
            {
                spriteTimerMax += 4;
            }

            if (spriteNum >= 5)
            {
                spriteTimerMax += 5;
            }
        }

        GetComponent<SpriteRenderer>().sprite = curSprite;
        spriteTimer++;
    }
}
