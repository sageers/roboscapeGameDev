using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorScript : MonoBehaviour
{
    //Von Lucas Trübisch

    public GameObject Elevator;
    public GameObject Door;
    public GameObject CamFocus;
    public Camera Cam;
    public GameObject Prof;
    public GameObject Robo;

    public SpriteRenderer FadeOut;

    static bool activated = false;
    public static void setActive()
    {
        activated = true;
    }

    int counter = 0;
    Vector2 camInit;
    Vector2 stepCamTransit;

    int jiggle = 0;

    private void Start()
    {
        activated = false;
        counter = 0;
        jiggle = 0;
    }

    void Update()
    {
        if (activated)
        {
            if (counter == 0)
            {
                RobotControllerLucas.lockMovementRobo = true;
                PlayerController2D.lockMovementProf = true;
                FollowChracter.lockCamera();
                FollowChracter.setElevatorTransition();
            }
            else if (counter > 30 && counter < 85)
            {
                Door.transform.localScale += new Vector3(0, 0.125f, 0);
                Door.transform.Translate(0, -0.03125f / 2f, 0);
            }
            else if (counter == 110)
            {
                camInit = Cam.transform.position;
                Vector2 CamFocus2D = CamFocus.transform.position;
                stepCamTransit = (CamFocus2D - camInit) / 60f;
            }
                
            else if (counter > 110 && counter <= 170)
            {
                Vector2 newCamPosition = (Vector2)Cam.transform.position + stepCamTransit;
                FollowChracter.setXY(newCamPosition);
            }
            else if(counter > 180 && counter < 330)
            {
                Vector3 jurney = new Vector3(0,0.1f,0);
                if (jiggle == 3)
                    jurney.y = 0.15f;
                if(jiggle == 4)
                {
                    jurney.y = 0.05f;
                    jiggle = -1;
                }
                jiggle++;
                Elevator.transform.Translate(jurney);
                Prof.transform.Translate(jurney);
                Robo.transform.Translate(jurney);

                if(counter <= 270)
                    FollowChracter.setXY((Vector2)Cam.transform.position + new Vector2(0,0.1f));
            }
            else if(counter > 350 && counter <= 390)
            {
                FadeOut.color += new Color(0,0,0,0.025f);
            }
            else if(counter == 391)
                SceneManager.LoadScene("Level1", LoadSceneMode.Single);
            counter++;
        }
    }
}
