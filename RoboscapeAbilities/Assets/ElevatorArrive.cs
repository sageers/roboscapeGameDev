using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorArrive : MonoBehaviour
{
    //Von Lucas Trübisch

    public GameObject Elevator;
    public GameObject Doors;
    //public GameObject CamFocus;
    public Camera Cam;
    public GameObject Prof;
    public GameObject Robo;

    static bool activated = true;
    int counter = 0;
    int jiggle = 0;

    void Start()
    {
        activated = true;
        jiggle = 0;
    }

    void Update()
    {
        if (activated)
        {
            if (counter <= 60)
            {
                Vector3 jurney = new Vector3(0, 0.1f, 0);
                if (jiggle == 3)
                    jurney.y = 0.15f;
                if (jiggle == 4)
                {
                    jurney.y = 0.05f;
                    jiggle = -1;
                }
                jiggle++;
                Elevator.transform.Translate(jurney);
                Prof.transform.Translate(jurney);
                Robo.transform.Translate(jurney);
            }
            else if(counter > 60 && counter <= 110)
            {
                Doors.transform.localScale -= new Vector3(0, 0.02f, 0);
                Doors.transform.Translate(0, 0.01f, 0);
            }

            counter++;

            if (counter > 110)
                activated = false;
            

        }
    }
}
