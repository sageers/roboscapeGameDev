using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{

    public static CollectibleManager instance;

    public int numberOfPotions;
    public int collectedPotions;

    public Text PotionText;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PotionText.text = "Potions: " + collectedPotions + " / " + numberOfPotions;
    }
}
