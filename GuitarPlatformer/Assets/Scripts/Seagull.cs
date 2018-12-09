using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : Obstacle {
    public Sprite diving;
    public Sprite landed;
    bool setDirection = false;

    protected override void Update()
    {
        base.Update();

        if (transform.position.x - player.transform.position.x < 8f && !setDirection)
        {
            GetComponent<SpriteRenderer>().sprite = diving;
            direction = (player.transform.position - transform.position).normalized;
            setDirection = true;
            transform.right = -direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<SpriteRenderer>().sprite = landed;
            direction = new Vector3(direction.x, 0, 0);
            transform.right = -direction;
        }

        else if (collision.gameObject.CompareTag("PlayerBullet"))
            Destroy(gameObject);

        else if (collision.gameObject.CompareTag("Player"))
            player.GetComponent<Player>().TakeDamage();
    }
}
