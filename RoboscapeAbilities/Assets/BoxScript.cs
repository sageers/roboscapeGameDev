using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public GameObject box;
    public Rigidbody2D boxRigidbody;
    int instanceID;

    void Start()
    {
        instanceID = box.GetInstanceID();
    }

    void Update()
    {
        if (GrabRobot.getGrabbedObject == instanceID || GrabberScript.getGrabbedObject == instanceID)
            boxRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        else {
            boxRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            boxRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            
        }
    }
}
