﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if the GameObject this is attached to hits a trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        //check which trigger is hit
        //maybe also check the GameObject which is hitting the trigger if necessary
        if (col.gameObject.CompareTag("buff"))
        {
            col.gameObject.SetActive(false);
            //extraStrength = true;         this is from the old function, with this new script it yet doesnt change anything
            //                              because this shall be done later with a seperate mechanic

        }
    }
}
