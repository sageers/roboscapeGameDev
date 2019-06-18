using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadmillScript : MonoBehaviour
{
    //Von Lucas Trübisch

    public GameObject Robot;

    public GameObject MillCable;
    public GameObject RopeCable;
    public GameObject RopeCableP1;
    public GameObject RopeCableP2;

    GameObject[] CogsTop;
    GameObject[] CogsBottom;
    GameObject[] CogsLeft;
    GameObject[] CogsRight;

    Vector3[] CogsTopX;
    Vector3[] CogsBottomX;
    Vector3[] CogsLeftY;
    Vector3[] CogsRightY;


    int CogCounter = 0;

    int visualUpdateCounter = 0;
    const int visualUpdateFrequency = 3;

    bool robotEntered = false;

    int ropeCounter = 0;
    const int ropeMax = 158;

    Vector3 millCablePosition;

    static bool profUnderPlatform = false;
    public static bool ProfUnderPlatform {
        set { profUnderPlatform = value; }
    }

    void Start()
    {
        CogsTop = GameObject.FindGameObjectsWithTag("CogTop");
        CogsBottom = GameObject.FindGameObjectsWithTag("CogBottom");
        CogsLeft = GameObject.FindGameObjectsWithTag("CogLeft");
        CogsRight = GameObject.FindGameObjectsWithTag("CogRight");

        CogsTopX = new Vector3[CogsTop.Length];
        CogsBottomX = new Vector3[CogsBottom.Length];
        CogsLeftY = new Vector3[CogsLeft.Length];
        CogsRightY = new Vector3[CogsRight.Length];
        for (int i = 0; i < CogsTop.Length; i++)
        {
            CogsTopX[i] = CogsTop[i].transform.position;
            CogsBottomX[i] = CogsBottom[i].transform.position;
        }
        for (int i = 0; i < CogsLeft.Length; i++)
        {
            CogsLeftY[i] = CogsLeft[i].transform.position;
            CogsRightY[i] = CogsRight[i].transform.position;
        }


        CogCounter = 0;

        robotEntered = false;

        ropeCounter = 0;

        millCablePosition = MillCable.transform.position;
    }

    
    void Update()
    {
        if (robotEntered)
        {
            float roboWalk = Input.GetAxis("Horizontal2");
            if(roboWalk > 0 && ropeCounter < ropeMax)
            {
                if (!RobotControllerLucas.lookingRight)
                    Flip();

                RobotControllerLucas.lockMovementRobo = true;

                visualUpdateCounter++;
                if(visualUpdateCounter == visualUpdateFrequency)
                {
                    visualUpdateCounter = 0;
                    CogCounter++;
                    if (CogCounter > 2)
                        CogCounter = 0;
                    TranslateCogs();
                }
                ropeCounter++;
                MillCable.transform.position = millCablePosition + new Vector3(0,(float)ropeCounter/50f, 0);
                RopeCable.transform.position = (RopeCableP1.transform.position + RopeCableP2.transform.position) / 2;
                RopeCable.transform.localScale = new Vector3(0.3f, Vector2.Distance(RopeCableP1.transform.position, RopeCableP2.transform.position)*2.2f,1);
            }
            else if(roboWalk < 0 && ropeCounter > 0)
            {
                if (RobotControllerLucas.lookingRight)
                    Flip();

                RobotControllerLucas.lockMovementRobo = true;

                if (!profUnderPlatform)
                {
                    visualUpdateCounter++;
                    if (visualUpdateCounter == visualUpdateFrequency)
                    {
                        visualUpdateCounter = 0;
                        CogCounter--;
                        if (CogCounter < 0)
                            CogCounter = 2;
                        TranslateCogs();
                    }
                    ropeCounter--;
                    MillCable.transform.position = millCablePosition + new Vector3(0, (float)ropeCounter / 50f, 0);
                    RopeCable.transform.position = (RopeCableP1.transform.position + RopeCableP2.transform.position) / 2;
                    RopeCable.transform.localScale = new Vector3(0.3f, Vector2.Distance(RopeCableP1.transform.position, RopeCableP2.transform.position) * 2.2f, 1);
                }
            }
            else
                RobotControllerLucas.lockMovementRobo = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == Robot)
        {
            robotEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == Robot)
        {
            robotEntered = false;
        }
    }

    void TranslateCogs()
    {
        for(int i = 0; i < CogsTop.Length; i++)
        {
            CogsTop[i].transform.position = CogsTopX[i] - new Vector3((float)CogCounter * 0.1f,0,0);
            CogsBottom[i].transform.position = CogsBottomX[i] + new Vector3((float)CogCounter * 0.1f, 0, 0);
        }
        for(int i = 0; i < CogsLeft.Length; i++)
        {
            CogsLeft[i].transform.position = CogsLeftY[i] - new Vector3(0, (float)CogCounter * 0.1f, 0);
            CogsRight[i].transform.position = CogsRightY[i] + new Vector3(0, (float)CogCounter * 0.1f, 0);
        }
    }

    public void Flip()
    {
        RobotControllerLucas.lookingRight = !RobotControllerLucas.lookingRight;
        Vector3 myScale = Robot.transform.localScale;
        myScale.x *= -1;
        Robot.transform.localScale = myScale;
    }
}
