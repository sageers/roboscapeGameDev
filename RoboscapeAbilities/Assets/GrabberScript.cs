using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberScript : MonoBehaviour
{
    //https://www.youtube.com/watch?v=SoBdvBTZqhw

    public bool grabbed;
    public float distance = 2f;
    public float throwForce;
    protected RaycastHit2D hit;
    public Transform holdPoint;
    public bool extraStrength = false;
    public Sprite playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //If something should be grabbed you have to press the right mous button
        if (Input.GetButtonDown("Fire2")){

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

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * throwForce;
                }
                //throw
            }
        }

        if (grabbed)
        {
            hit.collider.gameObject.transform.position = holdPoint.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }

    //collect buff extra strenght
    void OnTriggerEnter2D(Collider2D col1)
    {
        if (col1.gameObject.CompareTag("buff"))
        {
            col1.gameObject.SetActive(false);
            extraStrength = true;

        }
    }
}
