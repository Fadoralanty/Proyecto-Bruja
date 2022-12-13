using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour,IDataPersistance
{
    public string PlayLevelScene;
    public string ContinueLevelScene;
    public AudioMixer AudioMixer;
    private void Start()
    {
        AudioManager.instance.play("bg");
        SetVolume(1f);
    }

    public void Play()
    {
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(PlayLevelScene);
    }

    public void Continue()
    {
        SceneManager.LoadSceneAsync(ContinueLevelScene);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        ContinueLevelScene = data.SavedScene;
    }
    public void SetVolume(float volume)
    {
        if(volume==0){ volume+=0.00001f;}
        AudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); 
    }
    public void SaveData(ref GameData data)
    {
        
    }
}
