using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] int next_scene = 0;
    bool change = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!change)
        {
            GM_.instance.GetMembers.audio.PlaySFX("LevelComplete");
            GM_.instance.GetMembers.scene_mgr.LoadScene(next_scene);
            change = true;
        }
        
    }


}
