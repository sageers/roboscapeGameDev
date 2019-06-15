using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController2D: MonoBehaviour
{
    //Von Lucas Trübisch

    public GameObject Prof;

    public float speed = 0.05f;
    public float jumpHeight;

    enum Jump_modes { standing, jumping, falling}
    int jumpingMode = (int)Jump_modes.standing;
    int jumpCount = 0;
    const int jumpCountMax = 40;

    private float direction;
    private bool faceRight = true;

    private float profY_old;
    private bool storedJump = false;

    static bool lockMovement = true;
    public static bool lockMovementProf
    {
        get { return lockMovement; }
        set { lockMovement = value; }
    }

    void Start()
    {
        direction = transform.localScale.x;
        profY_old = Prof.transform.position.y;
        lockMovement = true;
    }

    void FixedUpdate()
    {
        if (lockMovement)
            return;


        //Rechts/Links Bewegung durch unsichtbare Wand begrenzen
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        int invisibleWall = FollowChracter.WallProf();
        if (!(moveHorizontal < 0 && invisibleWall < 0) && !(moveHorizontal > 0 && invisibleWall > 0))
            gameObject.transform.Translate(moveHorizontal * speed, 0, 0.0f);



        //Springen und Landen

        //Spieler-Eingabe "Jump"
        if (Input.GetButtonDown("Jump"))
        {
            if(jumpingMode == (int)Jump_modes.standing)
            {
                jumpingMode = (int)Jump_modes.jumping;
                jumpCount = 0;
            }
            else if (jumpingMode == (int)Jump_modes.falling)
                storedJump = true;
        }
        if(jumpingMode == (int)Jump_modes.standing && storedJump)
        {
            storedJump = false;
            jumpingMode = (int)Jump_modes.jumping;
            jumpCount = 0;
        }

        //Jump-Translation
        if(jumpingMode == (int)Jump_modes.jumping)
        {
            gameObject.transform.Translate(0, jumpHeight, 0.0f);
            jumpCount++;
            if (jumpCount >= jumpCountMax)
            {
                jumpCount = 0;
                jumpingMode = (int)Jump_modes.falling;
            }
        }

        //Update des Spring-Modus
        if (jumpingMode == (int)Jump_modes.jumping && Prof.transform.position.y <= profY_old)
            jumpingMode = (int)Jump_modes.falling;
        else if(jumpingMode == (int)Jump_modes.falling && Prof.transform.position.y >= profY_old)
            jumpingMode = (int)Jump_modes.standing;

        profY_old = Prof.transform.position.y;



        //Prof. in Gehrichtung drehen
        if(moveHorizontal < 0 && faceRight)
        {
            transform.localScale = new Vector2(-direction, transform.localScale.y);
            faceRight = false;
        }
        else if(moveHorizontal > 0 && !faceRight)
        {
            transform.localScale = new Vector2(direction, transform.localScale.y);
            faceRight = true;
        }
        
    }

}
