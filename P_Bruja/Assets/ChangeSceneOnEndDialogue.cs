using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEndDialogue : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void Start()
    {
        INK_Dialogue_Manager.instance.onDialogueFinished.AddListener(OnDialogueExitedListener);
    }
    // Update is called once per frame
    private void OnDialogueExitedListener()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
