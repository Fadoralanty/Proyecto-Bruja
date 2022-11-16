using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement; 

public class DataPersistanceManager : MonoBehaviour
{
    [Header("Debugging")] [SerializeField] private bool IsNewGame;
    public static DataPersistanceManager instance { get; private set; }
    private GameData _gameData;
    public delegate void Notify();
    public event Notify OnGameSaved;
    public GameData GameData => _gameData;

    private List<IDataPersistance> _dataPersistanceObjects;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadGame();
    }    
    public void OnSceneUnloaded(Scene scene)
    {
        //SaveGame();
    }
    private void Start()
    {
        _dataPersistanceObjects = FindAllDataPersistanceObjects();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistances);

    }

    public void NewGame()
    {
        _gameData = new GameData();
        SaveSystem.SaveGameData(_gameData);
    } 
    public void LoadGame()
    {
        _dataPersistanceObjects = FindAllDataPersistanceObjects();
        _gameData = SaveSystem.LoadGameData();
        if (IsNewGame)
        {
            NewGame();
        }
        if (_gameData == null)
        {
            Debug.Log("No data was found. Initializing to default values");
            return;
        }

        Debug.Log("there is data");
        foreach (IDataPersistance dataPersistanceObject in _dataPersistanceObjects)
        {
            dataPersistanceObject.LoadData(_gameData);
        }
    }   
    public void SaveGame()
    {
        if (_gameData == null) return;

        foreach (IDataPersistance dataPersistanceObject in _dataPersistanceObjects)
        {
            dataPersistanceObject.SaveData(ref _gameData);
        }
        
        SaveSystem.SaveGameData(_gameData);
        OnGameSaved?.Invoke();
    }
    
}
