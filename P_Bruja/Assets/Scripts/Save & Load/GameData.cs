using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    //playerControllerData
    public Vector3 playerPosition;
    public float playerCurrentLife;
    public List<Item> itemList;
    //GameManagerData
    public int _morality;
    public GameData()
    {
        playerPosition = Vector3.zero;
        itemList = new List<Item>();
        playerCurrentLife = 100;
        _morality = 0;
    }
}