using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : AutoCleanupSingleton<PauseManager>
{
    [SerializeField, Tooltip("Event called when pausing the game")] private UnityEvent _onPause = default;
    [SerializeField, Tooltip("Event called when unpausing")] private UnityEvent _onUnpause = default;
    public bool _isPaused { get; set; }

    public override void Awake()
    {
        base.Awake();
        UnPause();
    }

    public void TryPauseUnpuase()
    {

        if (GM_.instance.GetMembers.scene_mgr.GetCurrentSceneName() != CustomeSceneManager.SceneName.MainMenu)
            Pause();

    }

    //This function pauses the game and invokes the relevant events
    void Pause()
    {
        if (_isPaused)
        {
            UnPause();
        }
        else
        {
            _isPaused = true;
            _onPause?.Invoke();
            Time.timeScale = 0;
            Invoke("ResetTriggers", 1.0f);
        }
    }

    //This function unpauses the game and invokes the relevant events
    void UnPause()
    {
        _isPaused = false;
        Time.timeScale = 1;
        _onUnpause?.Invoke();
        Invoke("ResetTriggers", 1.0f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TryPauseUnpuase();
        }
    }
}
