using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmElement : MonoBehaviour
{
    int id;
    public GameObject AE_prefab;

    void Start()
    {
        id = RoboArmScript.NumArmElements;
        RoboArmScript.NumArmElements++;
        AE_prefab = GameObject.Find("ArmElement(Clone)");
        AE_prefab.name = "AE" + id.ToString();

        AE_prefab.transform.position = RoboArmScript.ArmStartPoint();
    }

    void Update()
    {
        AE_prefab.transform.position = RoboArmScript.get_AE_path(id - 1);
        int direction = RoboArmScript.get_AE_direction(id - 1);


        if (direction == (int)RoboArmScript.direction.left)
            AE_prefab.transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (direction == (int)RoboArmScript.direction.right)
            AE_prefab.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (direction == (int)RoboArmScript.direction.up)
            AE_prefab.transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (direction == (int)RoboArmScript.direction.down)
            AE_prefab.transform.rotation = Quaternion.Euler(0, 0, 270);
    }
}
