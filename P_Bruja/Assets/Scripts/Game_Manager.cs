using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour,IDataPersistance
{
    public static Game_Manager instance;
    
    [Range(-100, 100)] public int _morality;
    public bool isGamePaused;
    public bool isGameOver;
    public bool InCombat;
    [SerializeField] private Damageable playerDamageable;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject GameOverScreen;
    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        isGameOver = false;
        isGamePaused = false;
    }

    private void Start()
    {
        playerDamageable.onDie.AddListener(OnPlayerDieListener);
        GameOverScreen.SetActive(false);
    }

    void OnPlayerDieListener()
    {
        GameOverScreen.SetActive(true);
        isGameOver = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0f : 1f;
        }

    }

    public void LoadData(GameData data)
    {
        _morality = data._morality;
        isGamePaused = true;
        isGamePaused = false;
    }

    public void SaveData(ref GameData data)
    {
        data._morality = _morality;
    }
}
