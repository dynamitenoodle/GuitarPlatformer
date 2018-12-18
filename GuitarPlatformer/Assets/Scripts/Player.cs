using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour {
    #region Attributes
    //jumping
    bool inAir;

    // sliding
    bool isSlide;
    bool canShoot;
    public int slideTimerMax = 60;

	// strumming
    public float bpm;
    float slideScale;
    float strum;
	float prevStrum;
    [HideInInspector]
    public float Timer;

	// shooting
	public GameObject bulletPrefab;
    public GameObject shockwavePrefab;
    int shootTimer;
    int shootTimerMax;
    public int fireTimerMax = 5;
    public int shockwaveTimerMax = 60;

	// Audio Manager
	AudioManager audioManager;

    // Animation
    public List<Sprite> runSprites;
    int spriteIndex = 0;
    float animTimer;
    [SerializeField]
	[Range(15f, 60f)]
    float animTimerMax = 30f;

    // Damage and Health stuff
    int health = 10;
    bool invul = false;
    float invulTimer;
    [SerializeField]
    float invulTimerMax = 60;

    /// <summary>
    /// contains last 1s of strums
    /// </summary>
    public static List<float> StrumList = new List<float>();
    #endregion

    // Use this for initialization
    void Start() {
		audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
		
        prevStrum = 0;
        strum = 0;
        inAir = true;
        isSlide = false;

        slideScale = 1.5f;

        shootTimer = 0;
        GetComponent<SpriteRenderer>().sprite = runSprites[spriteIndex];
    }

    // Update is called once per frame
    void Update()
    {
		StrumTick();

        #region Input
        // Jumping
        if (GetGreenButton() && CheckAction())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500f));
            inAir = true;
        }

        // Sliding
        if (GetRedButton() && CanSlide())
        {

            isSlide = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / slideScale, transform.localScale.z);
        }

        else if (!GetRedButton() && isSlide)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * slideScale, transform.localScale.z);
            isSlide = false;
        }

        // Shooting
        if (GetBlueButton() && canShoot)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.GetChild(0).transform);
            newBullet.transform.parent = null;
            newBullet.transform.localScale = bulletPrefab.transform.localScale;
            canShoot = false;
            shootTimer = 0;
            shootTimerMax = fireTimerMax;
            audioManager.PlaySound("Shoot");
        }

        // Shockwave
        if (GetYellowButton() && canShoot)
        {
            GameObject newBullet = Instantiate(shockwavePrefab, transform);
            newBullet.transform.parent = null;
            newBullet.transform.localScale = shockwavePrefab.transform.localScale;
            canShoot = false;
            shootTimer = 0;
            shootTimerMax = shockwaveTimerMax;
            audioManager.PlaySound("Shockwave");
        }

        // Projectile timer
        if (!canShoot)
        {
            shootTimer++;
            if (shootTimer >= shootTimerMax)
            {
                canShoot = true;
            }
        }
        #endregion

        AnimationTick();
        HealthTick();
    }

    #region InputMethods
    bool GetGreenButton() { return Input.GetButtonDown("Green"); }
    bool GetRedButton() { return Input.GetButton("Red"); }
    bool GetYellowButton() { return Input.GetButtonDown("Yellow"); }
    bool GetBlueButton() { return Input.GetButtonDown("Blue"); }
    bool GetOrangeButton() { return Input.GetButtonDown("Orange"); }
    bool GetStart() { return Input.GetButtonDown("Start"); }
    float GetStrum() { return Input.GetAxisRaw("Horizontal"); }
    #endregion

    /// <summary>
    /// Checks to see if we can slide
    /// </summary>
    /// <returns></returns>
    bool CanSlide()
    {
        return CheckAction();
    }

    /// <summary>
    /// Checks to see if we can do an action
    /// </summary>
    /// <returns></returns>
    bool CheckAction() { return (!inAir && !isSlide); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inAir = false;
        }
    }

	/// <summary>
	/// Does the strum mechanics
	/// </summary>
	void StrumTick()
	{
		Timer += Time.deltaTime;
		prevStrum = strum;
		if (GetStrum() > .1f || GetStrum() < -.1f)
		{
			strum = 1;
		}
		else if (strum > 0)
		{
			strum--;
		}
		if (strum > 0 && prevStrum == 0)
		{
			StrumList.Add(Timer);
		}
        if (StrumList.Count > 2)
        {
            StrumList.RemoveAt(0);
        }
		foreach (float item in StrumList.ToArray())
		{
			if ((Timer - item) > 1f)
			{
				StrumList.RemoveAt(StrumList.IndexOf(item));
			}
		}
        float bps = 0;
        if (StrumList.Count > 1)
        {
            float gap = Mathf.Abs(StrumList[0] - StrumList[1]);
            bps = 1f / gap;
            if (Input.GetButton("Orange"))
            {
                bps /= 4f;
            }
        }
        else if (StrumList.Count > 0)
        {
            float gap = Mathf.Abs(Timer - StrumList[0]);
            if (gap <= .1f)
            {
                gap = (1/(bpm/60))/2f;
            }
            bps = 1f / gap;
            if (Input.GetButton("Orange"))
            {
                bps /= 4f;
            }
            
           
        }
        else
        {
            bps = 1f/-.8f;
        }
		float posx = (bps / (bpm / 60))/2;
        Vector3 targetpos = new Vector3(Screen.width * posx, 0,0)/4f;
		Vector3 v = GetComponent<Rigidbody2D>().velocity;
        float maxspeed = Mathf.Abs(bps);
        if (StrumList.Count == 0)
        {
            maxspeed = 3;
        }
        v = (targetpos-new Vector3(transform.position.x,0,0)).normalized*maxspeed+new Vector3(0,v.y,0);
        
        GetComponent<Rigidbody2D>().velocity = v;

		if (transform.position.x < -10f || transform.position.y < -5.0f)
        {
            transform.position = Vector3.zero;
            GameObject.Find("UI Canvas").transform.GetChild(0).GetComponent<ScoreMover>().ResetScore();
        }
			
	}

    void AnimationTick()
    {
        animTimer += (Time.deltaTime * 100f);
        float curVel = GetComponent<Rigidbody2D>().velocity.x;
        float max = 0; // temporary max value for ease of animation

        if (curVel == 0)
            max = animTimerMax;
        else if(curVel > 0)
            max = animTimerMax / Math.Abs(curVel);
        else if (curVel < 0)
            max = animTimerMax * Math.Abs(curVel);

        // should we move over to the next animation frame
        if (animTimer > max)
        {
            animTimer = 0;
            spriteIndex++;
            if (spriteIndex >= runSprites.Count)
                spriteIndex = 0;

            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = runSprites[spriteIndex];
        }
    }

    public void TakeDamage()
    {
        if (!invul)
        {
            invul = true;
            health--;
        }
    }

    void HealthTick()
    {
        // if invulerable
        if (invul)
        {
            invulTimer++;
            if (invulTimer < invulTimerMax / 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(-4.0f, GetComponent<Rigidbody2D>().velocity.y, 0);
            }

            // flicker the player
            if (invulTimer % 2 == 1)
                GetComponent<SpriteRenderer>().enabled = false;
            else
                GetComponent<SpriteRenderer>().enabled = true;

            // if we need to reset the hit thingy
            if (invulTimer > invulTimerMax)
            {
                GetComponent<SpriteRenderer>().enabled = true;
                invulTimer = 0;
                invul = false;
            }
        }
    }
}
