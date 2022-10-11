using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{   //TODO agregar mas funciones en torno nesesitemos mas opciones
    private GameObject _pauseCanvas;
    private void Start()
    {
        _pauseCanvas = gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        _pauseCanvas.SetActive(Game_Manager.instance.isGamePaused);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
