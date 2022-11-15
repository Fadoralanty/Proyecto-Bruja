using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    public bool reaadyToGO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && reaadyToGO == true)
        {
            DataPersistanceManager.instance.SaveGame();
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}
