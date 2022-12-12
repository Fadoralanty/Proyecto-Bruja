using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        CheckMorality();
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
        DataPersistanceManager.instance.OnGameLoaded += OnGameLoadedListener;
        playerDamageable.onDie.AddListener(OnPlayerDieListener);
        GameOverScreen.SetActive(false);
        Time.timeScale =  1f;
        AudioManager.instance.play("bg");
       // Debug.Log(SceneManager.GetActiveScene().name);
    }

    void OnPlayerDieListener()
    {
        GameOverScreen.SetActive(true);
        isGameOver = true;
    }

    void OnGameLoadedListener()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0f : 1f;
        }

    }
    public void MoralityPoints(int Points)
    {
        CheckMorality();
        _morality += Points;
        CheckMorality();
    }
    public void CheckMorality()
    {
        if(_morality < -100)
        {
            _morality = -100;
        }
        if(_morality > 100)
        {
            _morality = 100;
        }
    }

    public void LoadData(GameData data)
    {
        _morality = data._morality;
        isGamePaused = false;
    }

    public void SaveData(ref GameData data)
    {
        data._morality = _morality;
        data.SavedScene = SceneManager.GetActiveScene().name;
    }
}
