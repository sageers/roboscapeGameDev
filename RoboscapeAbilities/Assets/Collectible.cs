using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public Transform holdPoint;
    public static bool grabbedCollectible = false;
    public bool thisIsGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        CollectibleManager.instance.numberOfPotions++;
    }

    // Update is called once per frame
    void Update()
    {
        //change state to "not grabbed" if Collectible is set to ground by GrabberScript
        if (!grabbedCollectible && thisIsGrabbed)
        {
            thisIsGrabbed = false;
        }

        //change the position of the grabbed Collectible
        if (grabbedCollectible && thisIsGrabbed)
        {
            gameObject.transform.position = holdPoint.position;
        }

        //bisher werden alle Collectibles gegrabt und abgelegt und ein zweites mal ablegen scheint teilweise nicht zu funktionieren.
    }

    //if the GameObject this is attached to hits a trigger
    void OnTriggerEnter2D(Collider2D col)
    {

        if(col.tag == "robot")
        {
            print("robot");
            CollectibleManager.instance.collectedPotions++;
            gameObject.SetActive(false);

            //set all bools to not grabbed
            grabbedCollectible = false;
            thisIsGrabbed = false;
            GrabberScriptLucas.grabbed = false;

            //extraStrength = true;         this is from the old function, with this new script it yet doesnt change anything
            //                              because this shall be done later with a seperate mechanic
        }else if(col.tag == "scientist")
        {
            print("scientist");
            
            //set all bools to Grabbed
            grabbedCollectible = true;
            thisIsGrabbed = true;
            GrabberScriptLucas.grabbed = true;
            GrabberScriptLucas.setGrabbedObject(1);
            
            

        }



    }
}
