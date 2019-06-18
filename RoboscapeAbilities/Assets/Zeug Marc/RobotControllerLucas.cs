using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControllerLucas : MonoBehaviour
{
    //Von Lucas Trübisch und Marc Pilates

    public float maxSpeed = 4;
    public float jumpForce = 200;
    //public Transform groundCheck;
    //public LayerMask whatIsGround;

    [HideInInspector]
    public static bool lookingRight = true;

    private Rigidbody2D rb2d;
    //private Animator anim;
    //private bool isGrounded = false;
    private bool jump = false;

    public enum states { normal, greifArm };
    static int state = (int)states.normal;
    public static int roboterState
    {
        get { return state; }
        set { state = value; }
    }

    static bool lockMovement = true;
    public static bool lockMovementRobo
    {
        get { return lockMovement; }
        set { lockMovement = value; }
    }


    void Start()
    {
        lookingRight = true;
        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        lockMovement = true;
    }

    void FixedUpdate()
    {
        if (lockMovement)
            return;


        if (state == (int)states.normal)
        {
            float hor = Input.GetAxis("Horizontal2");
            //anim.SetFloat("Speed", Mathf.Abs(hor));

            int invisibleWall = FollowChracter.WallRobo();
            if (!(hor < 0 && invisibleWall < 0) && !(hor > 0 && invisibleWall > 0))
                rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);
            else
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);
            //anim.SetBool("isGrounded", isGrounded);

            if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
            {
                Flip();
            }

            if (jump)
            {
                rb2d.AddForce(new Vector2(0, jumpForce));
                jump = false;
            }
        }
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}