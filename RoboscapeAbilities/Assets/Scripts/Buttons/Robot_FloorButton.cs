using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot_FloorButton : MonoBehaviour
{

    protected bool pressed = false;
    protected SpriteRenderer spriteRend;


    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (!pressed && col.gameObject.tag == "robot")
        {

            Debug.Log("Collision with Player Robot");
            spriteRend.color = Color.green;
            pressed = true;


            //add switch case for different actions on different buttons
        }

    }


}
