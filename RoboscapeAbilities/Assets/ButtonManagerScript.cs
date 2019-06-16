using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour
{
    static int level = 0;

    public GameObject Prof;
    public GameObject Robo;

    //Level1
    public GameObject Gate1;
    public GameObject Gate2;
    public GameObject Gate3;
    public GameObject Platform;
    public GameObject Gate4;

    static bool stateButton1 = false;
    static bool stateButton2 = false;
    int gate1_moved = 0;

    static bool stateButton3 = false;
    int gate2_moved = 0;

    static bool stateButton4 = false;
    int gate3_moved = 0;

    static bool stateButton5 = false;
    static bool stateButton6 = false;
    int platform_moved_up = 0;
    int platform_moved_right = 0;

    static bool stateButtonPerma1 = false;
    int gate4_moved = 0;

    static bool stateButtonMulti1_0 = false;
    static bool stateButtonMulti1_1 = false;
    static bool stateButtonMulti1_both = false;
    public static bool stateButtonMulti1()
    {
        if (stateButtonMulti1_both)
            return true;
        else if (stateButtonMulti1_0 && stateButtonMulti1_1)
        {
            stateButtonMulti1_both = true;
            return true;
        }  
        else
            return false;
    }


    //Level2

    static bool stateButtonMulti2_0 = false;
    static bool stateButtonMulti2_1 = false;
    static bool stateButtonMulti2_2 = false;
    static bool stateButtonMulti2_all = false;
    public static bool stateButtonMulti2()
    {
        if (stateButtonMulti2_all)
            return true;
        else if (stateButtonMulti2_0 && stateButtonMulti2_1 && stateButtonMulti2_2)
        {
            stateButtonMulti2_all = true;
            return true;
        }
        else
            return false;
    }

    private void Start()
    {
        char[] level_char = SceneManager.GetActiveScene().name.ToCharArray();
        level = (int)char.GetNumericValue(level_char[level_char.Length-1]);
         

            if (level == 1)
        {
            stateButtonPerma1 = false;
            gate4_moved = 0;
            stateButtonMulti1_0 = false;
            stateButtonMulti1_1 = false;
            stateButtonMulti1_both = false;
        }
        else if(level == 2)
        {
            stateButtonMulti2_0 = false;
            stateButtonMulti2_1 = false;
            stateButtonMulti2_2 = false;
            stateButtonMulti2_all = false;
        }
    }


    private void Update()
    {
        if (level == 1)
        {
            //Gate1
            if (stateButton1 || stateButton2)
            {
                if (gate1_moved < 20)
                {
                    Gate1.transform.Translate(0, 0.15f, 0);
                    gate1_moved++;
                }
            }
            else
            {
                if (gate1_moved > 0)
                {
                    Gate1.transform.Translate(0, -0.15f, 0);
                    gate1_moved--;
                }
            }

            //Gate2
            if (stateButton3)
            {
                if (gate2_moved < 12)
                {
                    Gate2.transform.Translate(0, 0.15f, 0);
                    gate2_moved++;
                }
            }
            else
            {
                if (gate2_moved > 0)
                {
                    Gate2.transform.Translate(0, -0.15f, 0);
                    gate2_moved--;
                }
            }

            //Gate3
            if (stateButton4)
            {
                if (gate3_moved < 10)
                {
                    Gate3.transform.localScale = Gate3.transform.localScale - new Vector3(0, 0.2f, 0);
                    Gate3.transform.Translate(0, 0.1f, 0);
                    gate3_moved++;
                }
            }
            else
            {
                if (gate3_moved > 0)
                {
                    Gate3.transform.Translate(0, -0.1f, 0);
                    Gate3.transform.localScale = Gate3.transform.localScale + new Vector3(0, 0.2f, 0);
                    gate3_moved--;
                }
            }

            //Platform
            //Upwards Movement
            if (stateButton5 && platform_moved_right == 0)
            {
                if (platform_moved_up < 59)
                {
                    Platform.transform.Translate(0, 0.05f, 0);
                    if (MovingPlatformScript.getRobotCollision)
                        Robo.transform.Translate(0, 0.05f, 0);
                    if (MovingPlatformScript.getProfCollision)
                        Prof.transform.Translate(0, 0.05f, 0);
                    platform_moved_up++;
                }
            }
            else
            {
                if (platform_moved_up > 0)
                {
                    Platform.transform.Translate(0, -0.05f, 0);
                    if (MovingPlatformScript.getRobotCollision)
                        Robo.transform.Translate(0, -0.05f, 0);
                    if (MovingPlatformScript.getProfCollision)
                        Prof.transform.Translate(0, -0.05f, 0);
                    platform_moved_up--;
                }
            }
            //Sideways Movement
            if (stateButton6 && platform_moved_up == 0)
            {
                if (platform_moved_right < 90)
                {
                    Platform.transform.Translate(0.05f, 0, 0);
                    if (MovingPlatformScript.getRobotCollision)
                        Robo.transform.Translate(0.05f, 0, 0);
                    if (MovingPlatformScript.getProfCollision)
                        Prof.transform.Translate(0.05f, 0, 0);
                    platform_moved_right++;
                }
            }
            else
            {
                if (platform_moved_right > 0)
                {
                    Platform.transform.Translate(-0.05f, 0, 0);
                    if (MovingPlatformScript.getRobotCollision)
                        Robo.transform.Translate(-0.05f, 0, 0);
                    if (MovingPlatformScript.getProfCollision)
                        Prof.transform.Translate(-0.05f, 0, 0);
                    platform_moved_right--;
                }
            }

            //Gate4
            if (stateButtonPerma1)
            {
                if (gate4_moved < 10)
                {
                    Gate4.transform.Translate(0, 0.2f, 0);
                    gate4_moved++;
                }
            }

            //Elevator
            if (stateButtonMulti1_both)
                ElevatorScript.setActive();
        }

        else if(level == 2)
        {
            //Elevator
            if (stateButtonMulti2_all)
                ElevatorScript.setActive();
        }
    }

    public static void ButtonChange(string button, bool state)
    {
        if (level == 1)
        {
            if (button == "Button1")
                stateButton1 = state;
            else if (button == "Button2")
                stateButton2 = state;
            else if (button == "Button3")
                stateButton3 = state;
            else if (button == "Button4")
                stateButton4 = state;
            else if (button == "Button5")
                stateButton5 = state;
            else if (button == "Button6")
                stateButton6 = state;
            else if (button == "ButtonPerma1")
                stateButtonPerma1 = state;
            else if (button == "ButtonMulti1_0")
                stateButtonMulti1_0 = state;
            else if (button == "ButtonMulti1_1")
                stateButtonMulti1_1 = state;
        }
        else if(level == 2)
        {
            if (button == "ButtonMulti2_0")
                stateButtonMulti2_0 = state;
            else if (button == "ButtonMulti2_1")
                stateButtonMulti2_1 = state;
            else if (button == "ButtonMulti2_2")
                stateButtonMulti2_2 = state;
        }
    }

}
