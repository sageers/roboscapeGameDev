using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboFloatv2 : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float acc;
    public float groundAcc;
    public float fricSlow;

    public float floatingHeight;
    public float engineAcc;
    public float upSpeed;
    public float maxUpSpeed;

    public float gravity;

    public bool engineOn;

    public float dist;
    public float distThresh;
    public float rayCastOffSetX1;
    public float rayCastOffSetX2;
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
        groundAcc = acc / 10; //Horizontale Beschleunigung bei Berührung des Bodens
        fricSlow = 3; //Horizontale Verlangsamung, sobald nicht mehr beschleunigt wird

        floatingHeight = 2; //Höhe, ab der die Triebwerksleistung nachlässt
        engineAcc = 13; //Vertikale Beschleunigung
        upSpeed = 0; //Vertikale Geschwindigkeit
        maxUpSpeed = 3; //Vertikale Geschwindigkeitsbegrenzung

        gravity = 9.81f;

        engineOn = false;

        distThresh = 0.05f;
        rayCastOffSetX1 = bottomCollider.bounds.size.x /4;
        rayCastOffSetX2 = bottomCollider.bounds.size.x /1.5f;
        rayCastOffSetY = -2 * bottomCollider.bounds.size.y; //Notwendig, damit der RayCast nicht die Hitbox des eigenen Sprites trifft.

        frequency = 1.5f; //Frequenz der Schwingung während des Schwebens
        magnitude = 0.2f; //Ausschlag der Schwingung während des Schwebens

        thisRigidbody = GetComponent<Rigidbody2D>();
        thisRigidbody.gravityScale = 0;

        Physics2D.queriesHitTriggers = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        dist = CheckRaycast().distance; //Distanz bis zur nächsten Hitbox unterhalb des Sprites

        Engine();

        SideMovement();

        MoveSprite();
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
        Vector2 startingPosition1 = new Vector2(transform.position.x + rayCastOffSetX1, transform.position.y + rayCastOffSetY);
        Vector2 startingPosition2 = new Vector2(transform.position.x - rayCastOffSetX2, transform.position.y + rayCastOffSetY);

        RaycastHit2D hit1 = Physics2D.Raycast(startingPosition1, new Vector2(0, -1));
        RaycastHit2D hit2 = Physics2D.Raycast(startingPosition2, new Vector2(0, -1));

        Debug.Log("Distance: " + hit1.distance + " | " + hit2.distance);

        Debug.DrawLine(startingPosition1, hit1.point, Color.green, 0.5f); //Need to enable "Gizmos" in scene view, top right corner, in order to be visible.
        Debug.DrawLine(startingPosition2, hit2.point, Color.green, 0.5f);

        if (hit1.distance > hit2.distance)
        {
            return hit1;
        }
        else
        {
            return hit2;
        }
    }

    void SideMovement()
    {
        if (Input.GetAxis("Horizontal2") != 0)
        {
            if (Input.GetAxis("Horizontal2") > 0)
            {
                if (speed < 0)
                {
                    CalcSpeed(acc + acc);
                }
                else
                {
                    if(dist < distThresh)
                    {
                        CalcSpeed(groundAcc);
                    }
                    else
                    {
                        CalcSpeed(acc);
                    }
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
                    if (dist < distThresh)
                    {
                        CalcSpeed(-groundAcc);
                    }
                    else
                    {
                        CalcSpeed(-acc);
                    }
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
        rayCastOffSetX1 *= -1;
        rayCastOffSetX2 *= -1;
    }
}
