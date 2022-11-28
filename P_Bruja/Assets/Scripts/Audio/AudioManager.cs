using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance==null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in Sounds )
        {
            sound.Source=gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.clip;
            sound.Source.volume = sound.volume;
            sound.Source.pitch = sound.pitch;
            sound.Source.loop = sound.IsLooping;
        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " not found check name and inspector");
            return;
        }
        s.Source.Play();
    } 
}
