using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public GameObject Platform;
    public GameObject Robo;
    public GameObject Prof;

    bool anyRobotCollision = false;
    bool anyProfCollision = false;

    Vector3 collisionPointRobo;
    Vector3 collisionPointProf;

    static bool robotCollision = false;
    static bool profCollision = false;
    static public bool getRobotCollision
    {
        get { return robotCollision; }
    }
    static public bool getProfCollision
    {
        get { return profCollision; }
    }

    private void Update()
    {
        if (!anyRobotCollision && !anyProfCollision)
            return;
        float left = Platform.transform.position.x - Platform.transform.lossyScale.x / 2;
        float right = Platform.transform.position.x + Platform.transform.lossyScale.x / 2;
        if (anyRobotCollision)
        {
            if (collisionPointRobo.x > left && collisionPointRobo.x < right && collisionPointRobo.y > Platform.transform.position.y)
                robotCollision = true;
            else
                robotCollision = false;
        }
        if (anyProfCollision)
        {
            if (collisionPointProf.x > left && collisionPointProf.x < right && collisionPointProf.y > Platform.transform.position.y)
                profCollision = true;
            else
                profCollision = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "robot")
        {
            anyRobotCollision = true;
            collisionPointRobo = col.contacts[0].point;
        }
            
        if (col.gameObject.tag == "scientist")
        {
            anyProfCollision = true;
            collisionPointProf = col.contacts[0].point;
        }  
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "robot")
        {
            collisionPointRobo = col.contacts[0].point;
        }

        if (col.gameObject.tag == "scientist")
        {
            collisionPointProf = col.contacts[0].point;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "robot")
        {
            anyRobotCollision = false;
            robotCollision = false;
        }
            
        if (col.gameObject.tag == "scientist")
        {
            anyProfCollision = false;
            profCollision = false;
        }
            
    }

}
