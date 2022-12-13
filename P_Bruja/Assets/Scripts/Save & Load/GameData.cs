using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    //Settings
    public float volume;
    //playerControllerData
    public Vector3 playerPosition;
    public float playerCurrentLife;
    public List<Item> itemList;
    //GameManagerData
    public int _morality;
    public string SavedScene;
    //Tutorials
    public bool isControlsScreenSeen;
    public GameData()//the data of a new game
    {
        playerPosition = Vector3.zero;
        itemList = new List<Item>();
        playerCurrentLife = 100;
        _morality = 0;
        isControlsScreenSeen = false;
        SavedScene = "Tutorial Estacion de tren";
        volume = 1;
    }
}