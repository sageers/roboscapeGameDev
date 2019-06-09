using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController2D: MonoBehaviour
{
    public float speed = 0.05f;
    public float jumpHeight;

    enum Jump_modes { standing, jumping, falling}
    int jumpingMode = (int)Jump_modes.standing;
    int jumpCount = 0;
    int jumpCountMax = 40;

    private float direction;
    private float posx;

    void Start()
    {

        direction = transform.localScale.x;
        posx = transform.position.x;

    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        int invisibleWall = FollowChracter.WallProf();
        if (!(moveHorizontal < 0 && invisibleWall < 0) && !(moveHorizontal > 0 && invisibleWall > 0))
            gameObject.transform.Translate(moveHorizontal * speed, 0, 0.0f);


        if (Input.GetButtonDown("Jump"))
        {
            if(jumpingMode == (int)Jump_modes.standing)
            {
                jumpingMode = (int)Jump_modes.jumping;
            }
        }
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

        //Prof. drehen
        if (transform.position.x < posx)
        {
            transform.localScale = new Vector2(-direction, transform.localScale.y);
            
        }
        else if(transform.position.x > posx){

            transform.localScale = new Vector2(direction, transform.localScale.y);


        }


        posx = transform.position.x;
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (jumpingMode == (int)Jump_modes.jumping)
        {
            if (col.gameObject.tag == "Ceiling")
            {
                jumpingMode = (int)Jump_modes.falling;
            }
        }
        else if(jumpingMode == (int)Jump_modes.falling)
        {
            if (col.gameObject.tag == "Floor" || col.gameObject.tag == "robot")
            {
                jumpingMode = (int)Jump_modes.standing;
            }
        }
    }

    
}
