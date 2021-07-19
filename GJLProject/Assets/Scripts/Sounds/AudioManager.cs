using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] List<AudioSFX> SFX;
    [SerializeField] List<AudioSFX> MUSIC;

    // Start is called before the first frame update
    void Start()
    {
        foreach(AudioSFX s in SFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.is_looping;
        }

        foreach (AudioSFX s in MUSIC)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.is_looping;
        }

        PlayMusic("Music");
    }

    public void PlaySFX(string name)
    {
        foreach(AudioSFX sound in SFX)
        {
            if(sound.id == name)
            {
                sound.source.Play();
                break;
            }
        }
    }

    HashSet<Collider> previous_colliders = new HashSet<Collider>();
    public void PlaySFXCollisions(string name, Collider coll_a, Collider coll_b)
    {

        if(previous_colliders.Contains(coll_a) && previous_colliders.Contains(coll_b))
        {
            previous_colliders.Remove(coll_a);
            previous_colliders.Remove(coll_b);
            return;
        }

        previous_colliders.Add(coll_a);
        previous_colliders.Add(coll_b);

        PlaySFX(name);

    }

    public void PlayMusic(string name)
    {
        foreach (AudioSFX sound in MUSIC)
        {
            if (sound.id == name)
            {
                sound.source.Play();
                break;
            }
        }
    }

    public void ChangeSFXVolumes(float value)
    {
        foreach(AudioSFX sfx in SFX)
        {
            sfx.source.volume = value;
        }
    }

    public void ChangeMusicVolumes(float value)
    {
        foreach (AudioSFX sfx in MUSIC)
        {
            sfx.source.volume = value;
        }
    }

}
