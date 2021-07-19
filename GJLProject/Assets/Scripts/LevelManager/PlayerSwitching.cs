using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitching : MonoBehaviour
{

    [SerializeField] GameObject[] characters;
    [SerializeField] FollowCamera scene_camera;
    GameObject active_character;
    int charatcer_index = 0;

    private void Start()
    {

        foreach(GameObject obj in characters)
        {
            obj.GetComponent<CharacterMovement>().enabled = false;
        }

        ChangeCharacter();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //change character on mouse button click
        {
            charatcer_index++;

            if (charatcer_index > characters.Length -1)
                charatcer_index = 0;

            ChangeCharacter();

        }
    }

    void ChangeCharacter()
    {
        if(active_character)
        //disable the person controller script
        {
            active_character.GetComponent<CharacterMovement>().enabled = false;
        }

        //update the active_character variable
        active_character = characters[charatcer_index];

        //enable new characters third person script
        active_character.GetComponent<CharacterMovement>().enabled = true;


        active_character = characters[charatcer_index];

        //alter the camera's target
        scene_camera.follow_transform = active_character.transform;
        scene_camera.look_at = active_character.transform;


    }
}
