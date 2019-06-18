using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillCablePlatformScript : MonoBehaviour
{
    //Von Lucas Trübisch

    public GameObject Prof;


    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject == Prof)
        {
            if (Prof.transform.position.y < transform.position.y)
                TreadmillScript.ProfUnderPlatform = true;
            else
                TreadmillScript.ProfUnderPlatform = false;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject == Prof)
            TreadmillScript.ProfUnderPlatform = false;
    }
}
