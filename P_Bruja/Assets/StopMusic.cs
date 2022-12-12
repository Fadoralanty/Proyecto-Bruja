using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    private void Start()
    {
        foreach (var sound in AudioManager.instance.Sounds)
        {
            AudioManager.instance.Stop(sound.name);
        }
        AudioManager.instance.play("bg");
    }
}
