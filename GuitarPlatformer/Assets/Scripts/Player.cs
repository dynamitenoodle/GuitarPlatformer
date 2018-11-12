using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // attributes
    //jumping
    bool inAir;
    
    // sliding
    bool isSlide;
    bool canShoot;
    int slideTimer;
    public int slideTimerMax = 60;
    float slideScale;

    // shooting
    public GameObject bulletPrefab;
    public GameObject shockwavePrefab;
    int shootTimer;
    int shootTimerMax;
    public int fireTimerMax = 5;
    public int shockwaveTimerMax = 60;

    // Use this for initialization
    void Start() {
        inAir = true;
        isSlide = false;

        slideTimer = 0;
        slideScale = 1.5f;

        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Jumping
        if (GetGreenButton() && CheckAction())
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500f));
            inAir = true;
        }

        // Sliding
        if (GetRedButton() && CanSlide())
        {
            slideTimer = 0;
            isSlide = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / slideScale, transform.localScale.z);
        }

        else if (!CanSlide())
        {
            if (slideTimer >= slideTimerMax && isSlide)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * slideScale, transform.localScale.z);
                isSlide = false;
            }
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
        }

        // Shockwave
        if (GetYellowButton() && canShoot)
        {
            GameObject newBullet = Instantiate(shockwavePrefab, transform);
            newBullet.transform.parent = null;
            newBullet.transform.localScale = shockwavePrefab.transform.localScale;
            newBullet.transform.parent = transform;
            canShoot = false;
            shootTimer = 0;
            shootTimerMax = shockwaveTimerMax;
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
    }

    #region InputMethods
    bool GetGreenButton() { return Input.GetButtonDown("Green"); }
    bool GetRedButton() { return Input.GetButtonDown("Red"); }
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
        slideTimer++;
        return CheckAction() && slideTimer >= slideTimerMax;
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
}
