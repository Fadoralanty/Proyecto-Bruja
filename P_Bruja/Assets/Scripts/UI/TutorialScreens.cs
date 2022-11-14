using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreens : MonoBehaviour,IDataPersistance
{
    private bool IsSeen;
    private void Start()
    {
        Game_Manager.instance.isGamePaused = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || IsSeen)
        {
            Game_Manager.instance.isGamePaused = false;
            IsSeen = true;
            gameObject.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        IsSeen = data.isControlsScreenSeen;
    }

    public void SaveData(ref GameData data)
    {
        data.isControlsScreenSeen = IsSeen;
    }
}
