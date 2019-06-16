using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonMulti : MonoBehaviour
{
    //Von Lucas Trübisch

    public GameObject Button;
    protected bool pressed = false;
    bool pressedOld = false;
    bool allPressed = false;

    protected SpriteRenderer spriteRend;
    public BoxCollider2D RobotCollider;
    public BoxCollider2D ProfCollider;

    string name;
    int groupe;
    int number;

    int translateButton = 0;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        name = Button.name;
        char[] name_char = name.ToCharArray();
        groupe = name_char[name_char.Length - 3];
        number = name_char[name_char.Length - 1];

        pressed = false;
        pressedOld = false;
        allPressed = false;
    }

    private void Update()
    {
        allPressed = ButtonManagerScript.stateButtonMulti1();
        if (allPressed)
            pressed = true;

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

