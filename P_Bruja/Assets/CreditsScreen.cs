using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreen : MonoBehaviour
{
    [SerializeField]private GameObject Text;
    [SerializeField]private float _scrollSpeed;

    private void Start()
    {
        foreach (var sound in AudioManager.instance.Sounds)
        {
            AudioManager.instance.Stop(sound.name);
        }
        AudioManager.instance.play("end song");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space))
        {
            Text.transform.position += _scrollSpeed * 3 * Time.deltaTime * Vector3.up;
        }
        else
        {
            Text.transform.position += _scrollSpeed * Time.deltaTime * Vector3.up;
        }
        
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

