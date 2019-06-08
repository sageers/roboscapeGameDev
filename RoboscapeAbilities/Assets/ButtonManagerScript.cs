using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerScript : MonoBehaviour
{
    public GameObject Gate1;

    static bool stateButton1 = false;
    static bool stateButton2 = false;
    int gate1_moved = 0;

    private void Update()
    {
        if(stateButton1 || stateButton2)
        {
            if(gate1_moved < 20)
            {
                Gate1.transform.Translate(0, 0.15f, 0);
                gate1_moved++;
            }
        }
        else
        {
            if(gate1_moved > 0)
            {
                Gate1.transform.Translate(0, -0.15f, 0);
                gate1_moved--;
            }
        }
    }

    public static void ButtonChange(string button, bool state)
    {
        if (button == "Button1")
            stateButton1 = state;
        else if (button == "Button2")
            stateButton2 = state;
    }

}
