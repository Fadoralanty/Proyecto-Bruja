using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour,IDataPersistance
{
    public string PlayLevelScene;
    public string ContinueLevelScene;

    private void Start()
    {
        AudioManager.instance.play("bg");
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

    public void SaveData(ref GameData data)
    {
        
    }
}
