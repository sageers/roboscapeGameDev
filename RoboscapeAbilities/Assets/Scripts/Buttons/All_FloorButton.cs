using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class All_FloorButton : MonoBehaviour
{
    //Von Lucas Trübisch und Sarah Sörries

    public GameObject Button;
    protected bool pressed = false;
    bool pressedOld = false;

    protected SpriteRenderer spriteRend;
    public BoxCollider2D RobotCollider;
    public BoxCollider2D ProfCollider;

    string name;

    int translateButton = 0;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        name = Button.name;
    }

    private void Update()
    {
        if(pressed && translateButton < 15)
        {
            Button.transform.Translate(0, -0.015f, 0);
            translateButton++;
        }
        else if (!pressed && translateButton > 0)
        {
            Button.transform.Translate(0, 0.015f, 0);
            translateButton--;
        }

        if(pressedOld != pressed)
        {
            pressedOld = pressed;
            ButtonManagerScript.ButtonChange(name, pressed);
        }
    }

    void OnTriggerStay2D(Collider2D col1)
    {
        if(col1 == RobotCollider || col1 == ProfCollider)
        {
            if (!pressed && Math.Abs(col1.transform.position.x - Button.transform.position.x) < 0.3)
            {
                spriteRend.color = Color.green;
                pressed = true;
                col1.gameObject.transform.Translate(0, 0.15f, 0);
            }
            else if (pressed && Math.Abs(col1.transform.position.x - Button.transform.position.x) >= 0.3)
            {
                spriteRend.color = Color.white;
                pressed = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col2)
    {
        if (col2 != RobotCollider || col2 == ProfCollider)
        {
            spriteRend.color = Color.white;
            pressed = false;
        }

    }


}
