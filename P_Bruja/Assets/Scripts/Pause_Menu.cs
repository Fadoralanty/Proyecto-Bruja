using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{   
    private GameObject _pauseCanvas;
    private void Start()
    {
        _pauseCanvas = gameObject.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        _pauseCanvas.SetActive(Game_Manager.instance.isGamePaused);
    }

    public void Save()
    {
        DataPersistanceManager.instance.SaveGame();
    } 
    public void Load()
    {
        DataPersistanceManager.instance.LoadGame();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
