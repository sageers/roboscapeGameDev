using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All_FloorButton : MonoBehaviour
{

    protected bool pressed = false;
    protected SpriteRenderer spriteRend;


    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D col1)
    {

        
            Debug.Log("TriggerStay: All");
            spriteRend.color = Color.green;
            pressed = true;
        
    }

    void OnTriggerExit2D(Collider2D col2)
    {

            Debug.Log("TriggerExit: All");
            spriteRend.color = Color.white;
            pressed = false;
        

    }

    /*void OnCollisionEnter2D(Collision2D col)
    {
        if (!pressed)
        {

            Debug.Log("Collision with an entity");
            spriteRend.color = Color.green;
            pressed = true;

            //add switch case for different actions on different buttons

        }

    }*/


}
