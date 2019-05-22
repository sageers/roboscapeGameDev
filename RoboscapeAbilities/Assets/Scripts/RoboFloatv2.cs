using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboFloatv2 : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acc;
    public float fricSlow;

    public float floatingHeight;
    public float engineAcc;
    public float upSpeed;
    public float maxUpSpeed;

    public float gravity;

    public bool engineOn;

    public float dist;
    public float distThresh;
    public float rayCastOffSetX;
    public float rayCastOffSetY;

    public float frequency;
    public float magnitude;

    public Collider2D bottomCollider;
    public Rigidbody2D thisRigidbody;

    [HideInInspector]
    public bool lookingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0; //Horizontale Geschwindigkeit
        maxSpeed = 4; //Horizontale Geschwindigkeitsbegrenzung
        acc = 4; //Horizontale Beschleunigung
        fricSlow = 3; //Horizontale Verlangsamung, sobald nicht mehr beschleunigt wird

        upSpeed = 0; //Vertikale Geschwindigkeit
        floatingHeight = 2; //Höhe, ab der die Triebwerksleistung nachlässt
        engineAcc = 13f; //Vertikale Beschleunigung
        maxUpSpeed = 3; //Vertikale Geschwindigkeitsbegrenzung

        gravity = 9.81f;

        engineOn = false;

        distThresh = 0.05f;
        rayCastOffSetX = bottomCollider.bounds.size.x / 2;
        rayCastOffSetY = -2 * bottomCollider.bounds.size.y; //Notwendig, damit der RayCast nicht die Hitbox des eigenen Sprites trifft.

        frequency = 1.5f; //Frequenz der Schwingung während des Schwebens
        magnitude = 0.2f; //Ausschlag der Schwingung während des Schwebens

        thisRigidbody = GetComponent<Rigidbody2D>();
        thisRigidbody.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = CheckRaycast().distance; //Distanz bis zur nächsten Hitbox unterhalb des Sprites

        Engine();

        SideMovement();

        MoveSprite();

        //Debug.Log("Dist: " + dist);
        //Debug.Log("upSpeed: " + upSpeed);
    }

    void MoveSprite()
    {
        transform.Translate(new Vector2(speed * Time.deltaTime, upSpeed * Time.deltaTime));
    }

    void Engine()
    {
        if (engineOn)
        {
            CalcFloating(engineAcc);

            if (Input.GetAxis("Vertical") < 0)
            {
                engineOn = false;
            }
        }
        else
        {
            CalcFloating(0); //Keine Beschleunigung wenn Engine aus ist

            if (Input.GetAxis("Vertical") > 0)
            {
                engineOn = true;
            }
        }
    }

    void CalcFloating(float floatingAcc)
    {
        if(dist < distThresh)
        {
            upSpeed = 0;
        }

        if (dist > floatingHeight && engineOn)
        {
            if(upSpeed > -magnitude)
            {
                if(upSpeed < magnitude)
                {
                    upSpeed = Mathf.Sin(Time.time * frequency) * magnitude; //Stoppt Sprite in der Luft und lässt leicht vertikal schwingen
                    return;
                }    
                floatingAcc = floatingHeight - dist * 2 + gravity; //Bremst oberhalb der floatingHeight
            }
        }

        float newSpeed = (floatingAcc - gravity) * Time.deltaTime + upSpeed; //Normale Geschwindigkeitsberechnung

        if(newSpeed > maxUpSpeed)
        {
            upSpeed = maxUpSpeed;
        }
        else if(newSpeed < (-maxUpSpeed))
        {
            upSpeed = -maxUpSpeed;
        }
            
        else
        {
            upSpeed = newSpeed;
        }
    }

    private RaycastHit2D CheckRaycast()
    {
        Vector2 startingPosition = new Vector2(transform.position.x + rayCastOffSetX, transform.position.y + rayCastOffSetY);

        return Physics2D.Raycast(startingPosition, new Vector2(0, -1), 10000);
    }

    void SideMovement()
    {
        if (Input.GetAxis("Horizontal2") != 0 && dist > distThresh)
        {
            if (Input.GetAxis("Horizontal2") > 0)
            {
                if (speed < 0)
                {
                    CalcSpeed(acc + acc);
                }
                else
                {
                    CalcSpeed(acc);
                }

                if(!lookingRight)
                {
                    Flip();
                }

            }
            if (Input.GetAxis("Horizontal2") < 0)
            {
                if (speed > 0)
                {
                    CalcSpeed(-acc - acc);
                }
                else
                {
                    CalcSpeed(-acc);
                }

                if (lookingRight)
                {
                    Flip();
                }

            }
        }
        else
        {
            SlowDown();
        }
    }

    void CalcSpeed(float acceleration)
    {
        speed = acceleration * Time.deltaTime + speed;
        if (speed > 0 && speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        else if (speed < 0 && speed < (-maxSpeed))
        {
            speed = -maxSpeed;
        }
    }

    void SlowDown()
    {
        if (speed != 0)
        {
            if (speed < 0)
            {
                if (speed > -0.5f)
                {
                    speed = 0;
                }
                else if(dist < distThresh)
                {
                    CalcSpeed(fricSlow *2);
                }
                else
                {
                    CalcSpeed(fricSlow);
                }
            }
            else
            {
                if (speed < 0.5f)
                {
                    speed = 0;
                }
                else if (dist < distThresh)
                {
                    CalcSpeed(-fricSlow * 2);
                }
                else
                {
                    CalcSpeed(-fricSlow);
                }
            }
        }
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale;
        myScale.x *= -1;
        transform.localScale = myScale;
        rayCastOffSetX *= -1;
    }
}
