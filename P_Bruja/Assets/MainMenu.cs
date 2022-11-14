using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string PlayLevelScene;
    public void Play()
    {
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadSceneAsync(PlayLevelScene);
    }

    public void Continue()
    {
        SceneManager.LoadSceneAsync(PlayLevelScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
