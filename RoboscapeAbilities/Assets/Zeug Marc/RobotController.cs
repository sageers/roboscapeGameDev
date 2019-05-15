using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public float maxSpeed = 4;
    public float jumpForce = 200;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    [HideInInspector]
    public bool lookingRight = true;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded = false;
    private bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JumpTast1") && isGrounded)
        {
            jump = true;
        }
        if (Input.GetButtonDown("JumpPad1") && isGrounded)
        {
            jump = true;
        }

    }

    void FixedUpdate()
    {
        //float hor = Input.GetAxis("Horizontal1");
        float hor = Input.GetAxis("Horizontal2");
        anim.SetFloat("Speed", Mathf.Abs(hor));
        rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);

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

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}