using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDetection : MonoBehaviour
{

    public float fovAngle = 110f;
    public float maxDetectionRange = 1000;
    public bool detected = false;

    public CapsuleCollider2D col;

    [HideInInspector]
    public bool lookingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("Detected: " + detected);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("OnTriggerStay");

        if(other.tag == "scientist" || other.tag == "robot")
        {
            Debug.Log("Player Hit");
            
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
