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

    /*void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("entered");

        if (col.gameObject.tag == "robot")
        {

            Debug.Log("TriggerStay: Player Robot");
            spriteRend.color = Color.green;
            pressed = true;


        }
    }*/

    void OnTriggerStay2D(Collider2D col1)
    {

        if(col1.gameObject.tag == "robot")
        {

            Debug.Log("TriggerStay: Player Robot");
            spriteRend.color = Color.green;
            pressed = true;

            
        }
    }

    void OnTriggerExit2D(Collider2D col2)
    {
        //
        if (col2.gameObject.tag == "robot")
        {

            Debug.Log("TriggerExit: Player Robot");
            spriteRend.color = Color.white;
            pressed = false;


        }

    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (!pressed && col.gameObject.tag == "robot")
        {

            Debug.Log("Collision with Player Robot");
            spriteRend.color = Color.green;
            pressed = true;


            //add switch case for different actions on different buttons
        }

    }*/


}
