using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class Pause_Menu : MonoBehaviour ,IDataPersistance
{
    [SerializeField] private TextMeshProUGUI GameSavedText;
    [SerializeField] private Slider _volumeSlider;
    public AudioMixer AudioMixer;
    [SerializeField]private float _volume;
    private GameObject _pauseCanvas;
    private void Start()
    {
        GameSavedText.gameObject.SetActive(false);
        _pauseCanvas = gameObject.transform.GetChild(0).gameObject;
        DataPersistanceManager.instance.OnGameSaved += OnSaveListener;
        _volumeSlider.value = _volume;
        
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

    public void SetVolume(float volume)
    {
        if(volume==0){ volume+=0.00001f;}
        _volume = volume;
        AudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
    }

    public void LoadData(GameData data)
    {
        _volume = data.volume;
    }

    public void SaveData(ref GameData data)
    {
        data.volume = _volume;
    }
}
