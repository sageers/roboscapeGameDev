using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonMulti : MonoBehaviour
{

    public GameObject Button;
    protected bool pressed = false;
    bool pressedOld = false;
    bool allPressed = false;

    protected SpriteRenderer spriteRend;
    public BoxCollider2D RobotCollider;
    public BoxCollider2D ProfCollider;

    string name;
    char groupe;
    char number;

    int translateButton = 0;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        name = Button.name;
        char[] name_char = name.ToCharArray();
        groupe = name_char[name_char.Length - 3];
        number = name_char[name_char.Length - 1];

        string aaaa = name + "  " + groupe +"   " +number;
        Debug.Log(aaaa);
    }

    private void Update()
    {
        if (allPressed)
            pressed = true;
        else if (groupe == 1 && ButtonManagerScript.stateButtonMulti1())
        {
            allPressed = true;
        }
        

        if (pressed && translateButton < 15)
        {
            Button.transform.Translate(0, -0.015f, 0);
            translateButton++;
        }
        if (allPressed)
            return;
        else if (!pressed && translateButton > 0)
        {
            Button.transform.Translate(0, 0.015f, 0);
            translateButton--;
        }

        if (pressedOld != pressed)
        {
            pressedOld = pressed;
            ButtonManagerScript.ButtonChange(name, pressed);
        }

        //Debug.Log("Button" + number + ":  " + pressed +", " + allPressed);
    }

    void OnTriggerStay2D(Collider2D col1)
    {
        if (allPressed)
            return;
        if (col1 == RobotCollider || col1 == ProfCollider)
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
        if (allPressed)
            return;
        if (col2 != RobotCollider || col2 == ProfCollider)
        {
            spriteRend.color = Color.white;
            pressed = false;
        }

    }

}

