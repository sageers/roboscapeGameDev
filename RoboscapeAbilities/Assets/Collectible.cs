using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CollectibleManager.instance.numberOfPotions++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //if the GameObject this is attached to hits a trigger
    void OnTriggerEnter2D(Collider2D col)
    {

        if(col.tag == "robot" || col.tag == "scientist")
        {
            CollectibleManager.instance.collectedPotions++;
            gameObject.SetActive(false);

            //extraStrength = true;         this is from the old function, with this new script it yet doesnt change anything
            //                              because this shall be done later with a seperate mechanic
        }



    }
}
