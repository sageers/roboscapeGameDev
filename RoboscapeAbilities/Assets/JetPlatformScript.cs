using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JetPlatformScript : MonoBehaviour
{
    public GameObject jetPlatform;
    public GameObject momentum1;
    public GameObject momentum2;
    public Rigidbody2D jetP_Rigidbody;


    const float sufficientDistance = 0.5f;
    const int sufficientAngle = 20;
    const float neutralForceDistance = 1f;

    float force1 = 0f;
    float force2 = 0f;

    const float maxForce = 12f;

    const float increase = 0.2f;
    const float decrease = 0.4f;

    enum user {non, prof, robo };
    int userJet1 = (int)user.prof;
    int userJet2 = (int)user.robo;

    bool oversteered = false;
    int next_point = 0;
    static Vector3[] path_back;

    void Start()
    {
        GameObject[] path_point_objects = GameObject.FindGameObjectsWithTag("PathPoint");
        path_back = new Vector3[path_point_objects.Length];
        for (int i = 0; i < path_point_objects.Length; i++)
        {
            char[] name_char = path_point_objects[i].name.ToCharArray();
            //int bla = name_char[name_char.Length - 1];
            //Debug.Log()
            path_back[10 * name_char[name_char.Length-2] + name_char[name_char.Length-1]] = path_point_objects[i].transform.position;
        }
    }

    void Update()
    {
        if (!oversteered)
        {
            //Update Force **************************************************
            bool jet1used = false;
            bool jet2used = false;
            if (Input.GetKey(KeyCode.A))//TODO ********************
            {
                if (userJet1 == (int)user.robo)
                {
                    force1 += increase;
                    jet1used = true;
                }
                else if (userJet2 == (int)user.robo)
                {
                    force2 += increase;
                    jet2used = true;
                }
            }
            if (Input.GetKey(KeyCode.S))//TODO ************
            {
                if (userJet1 == (int)user.prof)
                {
                    force1 += increase;
                    jet1used = true;
                }
                else if (userJet2 == (int)user.prof)
                {
                    force2 += increase;
                    jet2used = true;
                }
            }
            if (!jet1used)
            {
                force1 -= decrease;
                if (force1 < 0)
                    force1 = 0;
            }
            else if (force1 > maxForce)
                force1 = maxForce;
            if (!jet2used)
            {
                force2 -= decrease;
                if (force2 < 0)
                    force2 = 0;
            }
            else if (force2 > maxForce)
                force2 = maxForce;
            
            platformTransform();
            //**************************************************

            if ((jetPlatform.transform.eulerAngles.z < 270 && jetPlatform.transform.eulerAngles.z > 260) || (jetPlatform.transform.eulerAngles.z > 90 && jetPlatform.transform.eulerAngles.z < 100))
            {
                oversteered = true;
                int shortestDistanceID = 0;
                float shortestDistance = Vector3.Distance(jetPlatform.transform.position, path_back[0]);
                for(int i = 1; i < path_back.Length; i++)
                {
                    float distanceThis = Vector3.Distance(jetPlatform.transform.position, path_back[i]);
                    if (distanceThis < shortestDistance)
                    {
                        shortestDistanceID = i;
                        shortestDistance = distanceThis;
                    }
                }
                next_point = shortestDistanceID;
            }
        }

        //Case Oversteer (Angle over 90°) ***********************
        else if (oversteered) {

            bool x_up = path_back[next_point].x - jetPlatform.transform.position.x > 0;
            bool y_up = path_back[next_point].y - jetPlatform.transform.position.y > 0;

            if (jetPlatform.transform.eulerAngles.z > 180) {
                if (jetPlatform.transform.eulerAngles.z < 270)
                    jetPlatform.transform.Rotate(0, 0, 1);
                else if ((x_up && jetPlatform.transform.eulerAngles.z < 360 - sufficientAngle) || !x_up)
                {
                    if (y_up && (force1 + force2) / 2 < 10 + neutralForceDistance)
                        force2 += increase;
                    else if (y_up && (force1 + force2) / 2 > 10 + neutralForceDistance)
                        force1 -= decrease;
                    else if (!y_up && (force1 + force2) / 2 < 10 - neutralForceDistance)
                        force2 += increase;
                    else if (!y_up && (force1 + force2) / 2 > 10 - neutralForceDistance)
                        force1 -= decrease;
                }
                /*else
                {
                    if (y_up && (force1 + force2) / 2 < 10 + neutralForceDistance)
                    {
                        force1 += increase;
                        force2 += increase;
                    }
                    else if ((force1 + force2) / 2 > 10 + neutralForceDistance)
                    {
                        force1 -= decrease;
                        force2 -= decrease;
                    }
                    else if ((force1 + force2) / 2 < 10 - neutralForceDistance)
                    {
                        force1 += increase;
                        force2 += increase;
                    }
                        
                    else if (!y_up && (force1 + force2) / 2 > 10 - neutralForceDistance)
                    {
                        force1 -= decrease;
                        force2 -= decrease;
                    }
                }*/

            }
            else
            {
                if (jetPlatform.transform.eulerAngles.z > 90)
                {
                    jetPlatform.transform.Rotate(0, 0, -1);
                }
            }
            //range force

            platformTransform();

            if(Vector3.Distance(jetPlatform.transform.position,path_back[next_point]) < sufficientDistance)
            {
                if (next_point == 0)
                {
                    if (jetPlatform.transform.eulerAngles.z > sufficientAngle && jetPlatform.transform.eulerAngles.z < 180)
                        jetPlatform.transform.Rotate(0, 0, -1);
                    else if (jetPlatform.transform.eulerAngles.z < 360 - sufficientAngle && jetPlatform.transform.eulerAngles.z > 180)
                        jetPlatform.transform.Rotate(0, 0, 1);
                    else
                        oversteered = false;
                }
                else
                    next_point--;
            }
        }

    }

    //Calculate Jet-Platform Translation and Rotation ******************
    private void platformTransform()
    {
        float platformAngle = (jetPlatform.transform.eulerAngles.z + 90) / 180 * (float)Math.PI;
        Vector2 direction1 = new Vector3(force1 * (float)Math.Cos(platformAngle), force1 * (float)Math.Sin(platformAngle));
        Vector2 direction2 = new Vector3(force2 * (float)Math.Cos(platformAngle), force2 * (float)Math.Sin(platformAngle));
        jetPlatform.transform.Rotate(0, 0, (force2 - force1) / 8);
        Vector2 newForce = new Vector2((direction1.x + direction2.x) / 6, (direction1.y + direction2.y) / 2);
        jetP_Rigidbody.AddForce(newForce);
    }
    //***************************************************************
}
