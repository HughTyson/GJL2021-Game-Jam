﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //change the scene
    public void PlayGame()
    {
        GM_.instance.GetMembers.scene_mgr.LoadScene(1);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
