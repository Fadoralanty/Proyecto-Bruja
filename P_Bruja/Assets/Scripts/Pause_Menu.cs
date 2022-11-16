using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GameSavedText;
    private GameObject _pauseCanvas;
    private void Start()
    {
        GameSavedText.gameObject.SetActive(false);
        _pauseCanvas = gameObject.transform.GetChild(0).gameObject;
        DataPersistanceManager.instance.OnGameSaved += OnSaveListener;
    }

    private void OnDisable()
    {
        DataPersistanceManager.instance.OnGameSaved -= OnSaveListener;
    }

    private void Update()
    {
        _pauseCanvas.SetActive(Game_Manager.instance.isGamePaused);
    }

    public void Save()
    {
        DataPersistanceManager.instance.SaveGame();
    }

    public void OnSaveListener()
    {
        StartCoroutine(ShowGameWasSaved(2));
    }

    IEnumerator ShowGameWasSaved(float t)
    {
        GameSavedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(t);
        GameSavedText.gameObject.SetActive(false);
        
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
