﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class FollowChracter : MonoBehaviour
{
    //Von Lucas Trübisch

    new public GameObject camera;
    public GameObject prof;
    public GameObject robo;
    //hälfte des Kamera-Blickfeldes in X-/Y-Richtung, bzw. range bis zur Barriere, an der sich die Kamera anfängt, zu verschieben
    public float cameraRangeX;
    public float cameraRangeY;

    //So weit können beide von einander entfernt stehen
    static float invisibleWall = 11.5f;
    static Vector2 profVec;
    static Vector2 roboVec;

    public SpriteRenderer FadeIn;

    public GameObject CamStartPoint;


    static float x;
    static float y;
    static float z;

    public static void setXY(Vector2 xy)
    {
        x = xy.x;
        y = xy.y;
    }

    int level = 0;

    bool allowTransitionsX = true;
    bool allowTransitionsY = true;

    float level1_offsetY = 2;
    float level2_offsetY = 2;

    static bool freeCamera = false;
    public static void lockCamera()
    {
        freeCamera = false;
    }

    GameObject[] CameraTransitions;
    GameObject[] CameraTargets;
    GameObject[] ResetTransitions;
    enum trans_modes {non, doing, done };
    int[] translationMode;
    static int currentTransition = -2;
    public static void setElevatorTransition()
    {
        currentTransition = -2;
    }

    int fadeInCounter = 0;
    int[] fadeInLength = {40,120 };//Fade-In-Länge in Abhängigkeit vom Level

    void Start()
    {
        freeCamera = false;
        fadeInCounter = 0;
        currentTransition = -2;

        x = camera.transform.position.x;
        y = camera.transform.position.y;
        z = camera.transform.position.z;
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            level = 1;
            allowTransitionsY = false;
            y = (prof.transform.position.y + robo.transform.position.y) / 2 + level1_offsetY;
        }
        else if(SceneManager.GetActiveScene().name == "Level2")
        {
            level = 2;
            allowTransitionsY = false;
            y = CamStartPoint.transform.position.y;
        }
            

        CameraTransitions = GameObject.FindGameObjectsWithTag("CameraTransition");
        CameraTargets = new GameObject[CameraTransitions.Length];
        ResetTransitions = new GameObject[CameraTransitions.Length];
        for (int i = 0; i < CameraTransitions.Length; i++)
        {
            char[] CameraTransitions_Char = CameraTransitions[i].name.ToCharArray();
            CameraTargets[i] = GameObject.Find("CameraTarget" + CameraTransitions_Char[CameraTransitions_Char.Length-1]);
            ResetTransitions[i] = GameObject.Find("ResetTransition" + CameraTransitions_Char[CameraTransitions_Char.Length - 1]);
        }
        translationMode = new int[CameraTransitions.Length];
        for(int i = 0; i < translationMode.Length; i++)
        {
            translationMode[i] = (int)trans_modes.non;
        }

    }


    void Update()
    {
        profVec = prof.transform.position;
        roboVec = robo.transform.position;

        if (freeCamera)
        {
            for(int i = 0; i < CameraTransitions.Length; i++)
            {
                if(translationMode[i] == (int)trans_modes.non && Vector3.Distance(CameraTransitions[i].transform.position,(prof.transform.position + robo.transform.position)/2) < 0.25f)
                {
                    freeCamera = false;
                    currentTransition = i;
                    translationMode[i] = (int)trans_modes.doing;

                    goto Transition;
                }
            }

            for(int i = 0; i < ResetTransitions.Length; i++)
            {
                if (translationMode[i] == (int)trans_modes.done && Vector2.Distance(ResetTransitions[i].transform.position, (robo.transform.position + prof.transform.position) / 2) < 0.25f)
                    translationMode[i] = (int)trans_modes.non;
            }


            if (robo.transform.position.x < camera.transform.position.x - cameraRangeX)
            {
                if (prof.transform.position.x < camera.transform.position.x - cameraRangeX)
                {
                    if (robo.transform.position.x < prof.transform.position.x)
                        x = robo.transform.position.x + cameraRangeX;
                    else
                        x = prof.transform.position.x + cameraRangeX;
                }
                else if (prof.transform.position.x > camera.transform.position.x + cameraRangeX)
                {
                    x = (prof.transform.position.x + robo.transform.position.x) / 2;
                }
                else
                    x = robo.transform.position.x + cameraRangeX;
            }
            else if (robo.transform.position.x > camera.transform.position.x + cameraRangeX)
            {
                if (prof.transform.position.x > camera.transform.position.x + cameraRangeX)
                {
                    if (robo.transform.position.x > prof.transform.position.x)
                        x = robo.transform.position.x - cameraRangeX;
                    else
                        x = prof.transform.position.x - cameraRangeX;
                }
                else if (prof.transform.position.x < camera.transform.position.x - cameraRangeX)
                {
                    x = (prof.transform.position.x + robo.transform.position.x) / 2;
                }
                else
                    x = robo.transform.position.x - cameraRangeX;
            }
            else if (prof.transform.position.x < camera.transform.position.x - cameraRangeX)
                x = prof.transform.position.x + cameraRangeX;
            else if (prof.transform.position.x > camera.transform.position.x + cameraRangeX)
                x = prof.transform.position.x - cameraRangeX;

            /*if (robo.transform.position.y < camera.transform.position.y - cameraRangeY)
            {
                if (prof.transform.position.y < camera.transform.position.y - cameraRangeY)
                {
                    if (robo.transform.position.y < prof.transform.position.y)
                        y = robo.transform.position.y + cameraRangeY;
                    else
                        y = prof.transform.position.y + cameraRangeY;
                }
                else if (prof.transform.position.y > camera.transform.position.y + cameraRangeY)
                {
                    y = (prof.transform.position.y + robo.transform.position.y) / 2;
                }
                else
                    y = robo.transform.position.y + cameraRangeY;
            }
            else if (robo.transform.position.y > camera.transform.position.y + cameraRangeY)
            {
                if (prof.transform.position.y > camera.transform.position.y + cameraRangeY)
                {
                    if (robo.transform.position.y > prof.transform.position.y)
                        y = robo.transform.position.y - cameraRangeY;
                    else
                        y = prof.transform.position.y - cameraRangeY;
                }
                else if (prof.transform.position.y < camera.transform.position.y - cameraRangeY)
                {
                    y = (prof.transform.position.y + robo.transform.position.y) / 2;
                }
                else
                    y = robo.transform.position.y - cameraRangeY;
            }
            else if (prof.transform.position.y < camera.transform.position.y - cameraRangeY)
                y = prof.transform.position.y + cameraRangeY;
            else if (prof.transform.position.y > camera.transform.position.y + cameraRangeY)
                y = prof.transform.position.y - cameraRangeY;*/

        }
        if(!freeCamera && fadeInCounter < fadeInLength[level-1])
        {
            FadeIn.color -= new Color(0, 0, 0, 0.025f);
            if(fadeInCounter == fadeInLength[level - 1] -1)
            {
                freeCamera = true;
                PlayerController2D.lockMovementProf = false;
                RobotControllerLucas.lockMovementRobo = false;
            }
            fadeInCounter++;
        }

    Transition:
        if (!freeCamera && currentTransition >= 0) {
            if (Vector2.Distance(camera.transform.position, CameraTargets[currentTransition].transform.position) > 0.2f)
            {
                if (allowTransitionsX)
                {
                    float x_ = camera.transform.position.x - CameraTargets[currentTransition].transform.position.x;
                    if (x_ > 0.2f)
                        x = camera.transform.position.x - 0.1f;
                    else if (x_ < 0.2f)
                        x = camera.transform.position.x + 0.1f;
                }
                if (allowTransitionsY)
                {
                    float y_ = camera.transform.position.y - CameraTargets[currentTransition].transform.position.y;
                    if (y_ > 0.2f)
                        y = camera.transform.position.y - 0.1f;
                    else if (y_ < 0.2f)
                        y = camera.transform.position.y + 0.1f;
                }
            }
            else
            {
                freeCamera = true;
                translationMode[currentTransition] = (int)trans_modes.done;
                currentTransition = -1;
                
            }
        }


        if(level == 1)
        {
            
        }else if(level == 2)
        {

        }
        else 
            y = (prof.transform.position.y + robo.transform.position.y) / 2;
        


        camera.transform.position = new Vector3(x,y,z);

    }

    //Check für Invisible Wall: 
    //Wenn Spieler zu weit links -> return -1
    //Wenn Spieler zu weit rechts -> return 1
    //else -> return 0
    public static int WallProf()
    {
        if (Vector2.Distance(profVec, roboVec) >= invisibleWall)
        {
            if (profVec.x < roboVec.x)
                return -1;
            else
                return 1;
        }
        else
            return 0;
    }
    public static int WallRobo()
    {
        if (Vector2.Distance(profVec, roboVec) >= invisibleWall)
        {
            if (profVec.x < roboVec.x)
                return 1;
            else
                return -1;
        }
        else
            return 0;
    }

}
