using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowChracter : MonoBehaviour
{
    new public GameObject camera;
    public GameObject prof;
    public GameObject robo;
    //hälfte des Kamera-Blickfeldes in X-/Y-Richtung, bzw. range bis zur Barriere, an der sich die Kamera anfängt, zu verschieben
    public float cameraRangeX;
    public float cameraRangeY;
    float x;
    float y;
    float z;

    void Start()
    {
        x = camera.transform.position.x;
        y = camera.transform.position.y;
        z = camera.transform.position.z;
    }


    void Update()
    {

        if (robo.transform.position.x < camera.transform.position.x - cameraRangeX)
        {
            if (prof.transform.position.x < camera.transform.position.x - cameraRangeX)
            {
                if (robo.transform.position.x < prof.transform.position.x)
                    x = robo.transform.position.x + cameraRangeX;
                else
                    x = prof.transform.position.x + cameraRangeX;
            }
            else if (prof.transform.position.x > camera.transform.position.x + cameraRangeX)
            {
                x = (prof.transform.position.x + robo.transform.position.x) / 2;
            }
            else
                x = robo.transform.position.x + cameraRangeX;
        }
        else if (robo.transform.position.x > camera.transform.position.x + cameraRangeX)
        {
            if (prof.transform.position.x > camera.transform.position.x + cameraRangeX)
            {
                if (robo.transform.position.x > prof.transform.position.x)
                    x = robo.transform.position.x - cameraRangeX;
                else
                    x = prof.transform.position.x - cameraRangeX;
            }
            else if (prof.transform.position.x < camera.transform.position.x - cameraRangeX)
            {
                x = (prof.transform.position.x + robo.transform.position.x) / 2;
            }
            else
                x = robo.transform.position.x - cameraRangeX;
        }
        else if (prof.transform.position.x < camera.transform.position.x - cameraRangeX)
            x = prof.transform.position.x + cameraRangeX;
        else if (prof.transform.position.x > camera.transform.position.x + cameraRangeX)
            x = prof.transform.position.x - cameraRangeX;

        if (robo.transform.position.y < camera.transform.position.y - cameraRangeY)
        {
            if (prof.transform.position.y < camera.transform.position.y - cameraRangeY)
            {
                if (robo.transform.position.y < prof.transform.position.y)
                    y = robo.transform.position.y + cameraRangeY;
                else
                    y = prof.transform.position.y + cameraRangeY;
            }
            else if (prof.transform.position.y > camera.transform.position.y + cameraRangeY)
            {
                y = (prof.transform.position.y + robo.transform.position.y) / 2;
            }
            else
                y = robo.transform.position.y + cameraRangeY;
        }
        else if (robo.transform.position.y > camera.transform.position.y + cameraRangeY)
        {
            if (prof.transform.position.y > camera.transform.position.y + cameraRangeY)
            {
                if (robo.transform.position.y > prof.transform.position.y)
                    y = robo.transform.position.y - cameraRangeY;
                else
                    y = prof.transform.position.y - cameraRangeY;
            }
            else if (prof.transform.position.y < camera.transform.position.y - cameraRangeY)
            {
                y = (prof.transform.position.y + robo.transform.position.y) / 2;
            }
            else
                y = robo.transform.position.y - cameraRangeY;
        }
        else if (prof.transform.position.y < camera.transform.position.y - cameraRangeY)
            y = prof.transform.position.y + cameraRangeY;
        else if (prof.transform.position.y > camera.transform.position.y + cameraRangeY)
            y = prof.transform.position.y - cameraRangeY;

        camera.transform.position = new Vector3(x, y, z);

    }
}
