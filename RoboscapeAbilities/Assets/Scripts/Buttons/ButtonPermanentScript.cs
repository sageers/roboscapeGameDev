using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonPermanentScript : MonoBehaviour
{
    public GameObject Button;
    protected bool pressed = false;

    protected SpriteRenderer spriteRend;
    public BoxCollider2D RobotCollider;
    public BoxCollider2D ProfCollider;

    string buttonName;

    int translateButton = 0;

    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        buttonName = Button.name;
    }


    void Update()
    {
        if (pressed && translateButton < 15)
        {
            Button.transform.Translate(0, -0.015f, 0);
            translateButton++;
        }
    }

    void OnTriggerStay2D(Collider2D col1)
    {
        if (col1 == RobotCollider || col1 == ProfCollider)
        {
            if (!pressed && Math.Abs(col1.transform.position.x - Button.transform.position.x) < 0.3)
            {
                pressed = true;
                col1.gameObject.transform.Translate(0, 0.15f, 0);
                ButtonManagerScript.ButtonChange(buttonName, pressed);
            }
        }
    }

}
