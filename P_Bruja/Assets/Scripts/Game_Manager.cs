using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
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

    public void SaveGame()
    {
        SaveSystem.SavePlayer(player);
    }    
    public void LoadGame()
    {
        //player
        PlayerDATA playerData = SaveSystem.LoadPlayer();
        Vector3 position;
        position.x = playerData.position[0];
        position.y = playerData.position[1];
        position.z = playerData.position[2];
        player.transform.position = position;


    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            Time.timeScale = isGamePaused ? 0f : 1f;
        }

    }
}
