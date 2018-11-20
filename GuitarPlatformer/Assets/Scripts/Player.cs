﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // attributes
    //jumping
    float prevStrum;
    bool inAir;
    public static float Timer;
    // sliding
    bool isSlide;
    bool canShoot;
    int slideTimer;
    public int slideTimerMax = 60;
    public float bpm;
    float slideScale;
    float strum;
    // shooting
    public GameObject bulletPrefab;
    public GameObject shockwavePrefab;
    int shootTimer;
    int shootTimerMax;
    public int fireTimerMax = 5;
    public int shockwaveTimerMax = 60;
    /// <summary>
    /// contains last 1s of strums
    /// </summary>
    public static List<float> StrumList = new List<float>();

    // Use this for initialization
    void Start() {
        prevStrum = 0;
        Timer = 0;
        strum = 0;
        inAir = true;
        isSlide = false;

        slideTimer = 0;
        slideScale = 1.5f;

        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        prevStrum = strum;
        if (GetStrum() > .1f|| GetStrum() < -.1f)
        {
            strum = 1;
        }
        else if(strum>0)
        {
            strum--;
        }
        if (strum > 0&&prevStrum == 0)
        {
            StrumList.Add(Timer);
        }
        
        foreach (float item in StrumList.ToArray())
        {
            if ((Timer - item) > 1f)
            {
                StrumList.RemoveAt(StrumList.IndexOf(item));
            }
           
        }
        float velx = (StrumList.Count / (bpm / 60));

        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        v.x = velx*2-2;
        GetComponent<Rigidbody2D>().velocity = v;

        if (transform.position.x < -10f || transform.position.y < -5.0f)
            transform.position = Vector3.zero;

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

        else if (!GetRedButton()&&isSlide)
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
}