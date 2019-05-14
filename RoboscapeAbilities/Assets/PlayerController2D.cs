using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController2D: MonoBehaviour
{
    public float speed = 0.05f;
    public float jumpHeight;

    private float direction;
    private float posx;

    void Start()
    {
        
        direction = transform.localScale.x;
        posx = transform.position.x;

    }
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        
        

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.AddForce(movement * speed);
        gameObject.transform.Translate(moveHorizontal * speed, 0, 0.0f);

        if (Input.GetButton("Jump"))
        {
            gameObject.transform.Translate(moveHorizontal * speed, jumpHeight, 0.0f);
        }

        if (transform.position.x < posx)
        {
            transform.localScale = new Vector2(-direction, transform.localScale.y);

        }
        else
        {
            transform.localScale = new Vector2(direction, transform.localScale.y);
        }


        posx = transform.position.x;
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
       // Debug.Log("OnCollisionEnter2D");
    }
}
