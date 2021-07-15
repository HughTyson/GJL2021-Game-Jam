using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitching : MonoBehaviour
{

    [SerializeField] GameObject[] characters;
    GameObject active_character;
    int charatcer_index = 0;

    private void Start()
    {
        active_character = characters[charatcer_index];
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) //change character on mouse button click
        {
            charatcer_index++;

            if (charatcer_index > characters.Length -1)
                charatcer_index = 0;

            Debug.Log(charatcer_index);

            //disable the third person controller script
            //enable new characters third person script
            //update the active_character variable

            active_character = characters[charatcer_index];

            //alter the camera's target
        }
    }
}
