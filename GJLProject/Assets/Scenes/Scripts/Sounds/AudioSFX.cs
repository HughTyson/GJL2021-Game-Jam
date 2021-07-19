using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioSFX 
{

    public string id;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;

    public bool is_looping = false;

    [HideInInspector]
    public AudioSource source;


}
