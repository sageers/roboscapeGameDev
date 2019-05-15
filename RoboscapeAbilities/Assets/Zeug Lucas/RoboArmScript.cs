using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoboArmScript : MonoBehaviour


{
    //TODO Ersetzen
    KeyCode left_key = KeyCode.LeftArrow;
    KeyCode right_key = KeyCode.RightArrow;
    KeyCode up_key = KeyCode.UpArrow;
    KeyCode down_key = KeyCode.DownArrow;
    KeyCode greifarm_key = KeyCode.Keypad1;
    KeyCode reverse_key = KeyCode.B;

    public GameObject roboter;
    public GameObject hand;
    public GameObject armElement;

    enum greifarmStates { move, reverse };
    int greifarmState;

    public int armMaxLength = 200;
    public float armSpeed = 0.04f;

    static Vector3 roboterPosition;

    //Arm-Position relativ zum Roboter
    static float armStartOffsetX;
    static float armStartOffsetY;
    public static Vector3 ArmStartPoint()
    {
        return new Vector3(armStartOffsetX, armStartOffsetY, 0) + roboterPosition;
    }

    static int armCurrentLength;
    static int[] armPath;
    public enum direction { non, left, right, up, down };

    static int numArmElements;
    static public int NumArmElements
    {
        get { return numArmElements; }
        set { numArmElements = value; }
    }
    static int numArmElements_New;

    static Vector3[] AE_path;
    public static Vector3 get_AE_path(int index)
    {
        return AE_path[index];
    }
    static int[] AE_direction;
    public static int get_AE_direction(int index)
    {
        return AE_direction[index];
    }

    float AE_length;
    int AE_int_length;
    int offset;

    static string handName;
    public static string getHandName
    {
        get { return handName; }
    }
    static Vector3 handStartPosition;
    static Vector3 handPosition;
    public static Vector3 getHandPosition()
    {

        return handPosition;
    }


    void Start()
    {
        armCurrentLength = 0;
        armPath = new int[armMaxLength + 1];
        numArmElements = 0;
        handName = hand.name;

        armStartOffsetX = hand.transform.position.x - roboter.transform.position.x;
        armStartOffsetY = hand.transform.position.y - roboter.transform.position.y;

        AE_length = armElement.transform.lossyScale.y;
        AE_int_length = (int)(AE_length / armSpeed);
        int maxNum_AE = (int)((float)armMaxLength / (float)AE_int_length) + 1;
        AE_path = new Vector3[maxNum_AE];
        AE_direction = new int[maxNum_AE];
        offset = (int)((hand.transform.position.y / 2) / armSpeed);
    }


    void Update()
    {
        if (RobotController.roboterState == (int)RobotController.states.normal)
        {
            if (Input.GetKeyDown(greifarm_key))
            {
                RobotController.roboterState = (int)RobotController.states.greifArm;
                //roboter.

                //BLALALALALALAALALAL
                greifarmState = (int)greifarmStates.move;
                handStartPosition = hand.transform.position;
                hand.transform.position += new Vector3(armSpeed, 0, 0);
                armPath[armCurrentLength] = (int)direction.right;
                armCurrentLength++;
                roboterPosition = roboter.transform.position;
            }

        }

        else if (RobotController.roboterState == (int)RobotController.states.greifArm)
        {
            if (armCurrentLength < armMaxLength && greifarmState == (int)greifarmStates.move)
            {

                if (Input.GetKey(left_key))
                {
                    hand.transform.position += new Vector3(-armSpeed, 0, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 180);
                    armPath[armCurrentLength] = (int)direction.left;
                    armCurrentLength++;
                }
                else if (Input.GetKey(right_key))
                {
                    hand.transform.position += new Vector3(armSpeed, 0, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 0);
                    armPath[armCurrentLength] = (int)direction.right;
                    armCurrentLength++;
                }
                else if (Input.GetKey(up_key))
                {
                    hand.transform.position += new Vector3(0, armSpeed, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 90);
                    armPath[armCurrentLength] = (int)direction.up;
                    armCurrentLength++;
                }
                else if (Input.GetKey(down_key))
                {
                    hand.transform.position += new Vector3(0, -armSpeed, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 270);
                    armPath[armCurrentLength] = (int)direction.down;
                    armCurrentLength++;
                }
            }

            if (armCurrentLength > 0 && Input.GetKey(reverse_key))
            {
                greifarmState = (int)greifarmStates.reverse;
                if (armPath[armCurrentLength] == (int)direction.left)
                {
                    hand.transform.position += new Vector3(armSpeed, 0, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                else if (armPath[armCurrentLength] == (int)direction.right)
                {
                    hand.transform.position += new Vector3(-armSpeed, 0, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else if (armPath[armCurrentLength] == (int)direction.up)
                {
                    hand.transform.position += new Vector3(0, -armSpeed, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
                else if (armPath[armCurrentLength] == (int)direction.down)
                {
                    hand.transform.position += new Vector3(0, armSpeed, 0);
                    hand.transform.rotation = Quaternion.Euler(0, 0, 270);
                }
                armCurrentLength--;
                if (armCurrentLength == 0)
                {
                    hand.transform.position = handStartPosition;
                    hand.transform.rotation = Quaternion.Euler(0, 0, 0);
                    RobotController.roboterState = (int)RobotController.states.normal;
                }
            }
            else if (Input.GetKeyUp(reverse_key))
                greifarmState = (int)greifarmStates.move;

            updateAE_path();
        }

    }


    void updateAE_path()
    {

        numArmElements_New = (int)Math.Ceiling((float)armCurrentLength / (float)AE_int_length);
        if (numArmElements_New < numArmElements)
        {
            Destroy(GameObject.Find("AE" + numArmElements.ToString()));
            numArmElements--;
        }
        else if (numArmElements_New > numArmElements)
        {
            numArmElements++;
            Instantiate(armElement);
        }

        if (numArmElements < 1)
            return;

        Vector3 temp = ArmStartPoint();
        int first_AE_int_length = armCurrentLength - ((numArmElements - 1) * AE_int_length);
        int AE_index = numArmElements - 1;
        int i = 0;

        while (i < first_AE_int_length)
        {
            temp.x += armSpeed;
            i++;
        }
        AE_path[AE_index] = temp;
        AE_direction[AE_index] = (int)direction.right;
        AE_index--;

        int length_check = AE_int_length;
        while (i < AE_int_length + offset && i < armCurrentLength)
        {
            temp.x += armSpeed;
            if (length_check <= 1)
            {
                length_check = AE_int_length;
                AE_path[AE_index] = temp;
                AE_direction[AE_index] = (int)direction.right;
                AE_index--;
            }
            else
            {
                length_check--;
            }
            i++;
        }

        while (i < armCurrentLength)
        {
            if (armPath[i - AE_int_length - offset] == (int)direction.left)
                temp.x -= armSpeed;
            else if (armPath[i - AE_int_length - offset] == (int)direction.right)
                temp.x += armSpeed;
            else if (armPath[i - AE_int_length - offset] == (int)direction.up)
                temp.y += armSpeed;
            else if (armPath[i - AE_int_length - offset] == (int)direction.down)
                temp.y -= armSpeed;
            i++;

            if (length_check <= 1)
            {
                length_check = AE_int_length;
                AE_path[AE_index] = temp;

                if (armPath[i - AE_int_length - offset] == (int)direction.left)
                    AE_direction[AE_index] = (int)direction.left;
                else if (armPath[i - AE_int_length - offset] == (int)direction.right)
                    AE_direction[AE_index] = (int)direction.right;
                else if (armPath[i - AE_int_length - offset] == (int)direction.up)
                    AE_direction[AE_index] = (int)direction.up;
                else if (armPath[i - AE_int_length - offset] == (int)direction.down)
                    AE_direction[AE_index] = (int)direction.down;

                AE_index--;
            }
            else
            {
                length_check--;
            }
        }
        handPosition = hand.transform.position;
    }
}
