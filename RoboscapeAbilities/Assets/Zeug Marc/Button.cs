using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private BoxCollider2D bc2d;
    public GameObject Player;
    public GameObject Robot;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bc2d.IsTouching(Player.GetComponent<BoxCollider2D>())){
            print("yes");
        }
    }
}
