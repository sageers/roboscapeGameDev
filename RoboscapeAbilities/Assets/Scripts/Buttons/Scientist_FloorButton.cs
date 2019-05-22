using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist_FloorButton : MonoBehaviour
{

    protected bool pressed = false;
    protected SpriteRenderer spriteRend;


    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D col1)
    {

        if (col1.gameObject.tag == "scientist")
        {

            Debug.Log("TriggerStay: Player Scientist");
            spriteRend.color = Color.green;
            pressed = true;


        }
    }

    void OnTriggerExit2D(Collider2D col2)
    {
        //
        if (col2.gameObject.tag == "scientist")
        {

            Debug.Log("TriggerExit: Player Scientist");
            spriteRend.color = Color.white;
            pressed = false;


        }

    }

    /* void OnCollisionEnter2D(Collision2D col)
     {
         if (!pressed && col.gameObject.tag == "scientist")
         {

             Debug.Log("Collision with Player Scientist");
             spriteRend.color = Color.green;
             pressed = true;

             //add switch case for different actions on different buttons

         }

     }*/


}
