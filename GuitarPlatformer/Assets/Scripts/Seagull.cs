using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : Obstacle {
    public Sprite landed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<SpriteRenderer>().sprite = landed;
            direction = new Vector3(direction.x, 0, 0);
        }

        else if (collision.gameObject.CompareTag("PlayerBullet"))
            Destroy(gameObject);

        else if (collision.gameObject.CompareTag("Player"))
            player.GetComponent<Player>().TakeDamage();
    }
}
