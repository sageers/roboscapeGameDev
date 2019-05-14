﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{

    protected bool pressed = false;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer spriteRend;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if(!pressed && col.gameObject.tag == "grabbable")
        {
            
            Debug.Log("OnCollisionEnter2D");
            spriteRend.color = Color.green;
            pressed = true;
        }
        
    }

    
}
