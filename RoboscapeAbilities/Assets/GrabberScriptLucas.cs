using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScriptLucas : MonoBehaviour
{
    //https://www.youtube.com/watch?v=SoBdvBTZqhw

    public static bool grabbed;
    public float distance = 2f;
    public static float throwForce;
    protected RaycastHit2D hit;
    public Transform holdPoint;
    public static bool extraStrength = false;
    public Sprite playerSprite;

    //Lucas Kram: **********************************
    static int grabbedObject = -1;
    public static int getGrabbedObject
    {
        get { return grabbedObject; }
    }
    public static void setGrabbedObject(int idGrabbedObject)
    {
        grabbedObject = idGrabbedObject;
    }
    //***********************************************

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //If something should be grabbed you have to press the right mous button
        if (Input.GetButtonDown("Hold")){

            //First check if the Player is already grabbing sth

            if (!grabbed)
            {
                //if nothing is grabbed start a raycast from playerposition towards the direction the player is facing
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                //if the raycast hits a collider and the collider is marked as grabbable grab it
                if(hit.collider!= null && hit.collider.tag== "grabbable")
                {
                    //check the mass of the object
                    //normal scientist can move anything with mass <= 1
                    //steroid scientist can move anything with mass <= 5
                    float grabbableMass = 1f;

                    if (extraStrength)
                    {
                        grabbableMass = 5f;

                    }


                    if (hit.collider.attachedRigidbody.mass <= grabbableMass)
                    {
                        grabbed = true;
                    }



                }

                //grab
            }
            else if(grabbed)
            {
                grabbed = false;

                if(grabbedObject != 1)
                {
                    if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
                    }
                }
                else
                {
                    //absetzen von Tränken hier irgendwie
                    Collectible.grabbedCollectible = false;
                }

                
                //throw
            }
        }

        //Lucas Kram: Der Großteil dieses if-cases ist neu ************
        if (grabbed)
        {
            if (grabbedObject != 1)
            {
                hit.collider.gameObject.transform.position = holdPoint.position;
                grabbedObject = hit.collider.gameObject.GetInstanceID();
            }
            
        }
        else {
            grabbedObject = -1;
        }
        //****************************************************************
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

    //collect buff extra strenght
    /*void OnTriggerEnter2D(Collider2D col1)
    {
        if (col1.gameObject.CompareTag("buff"))
        {
            col1.gameObject.SetActive(false);
            extraStrength = true;

        }
    }*/
}
