using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=_nRzoTzeyxU&list=WL&index=22&t=4s
public class NpcDialogueTrigger : MonoBehaviour
{
    [Header("Visual Que")]
    [SerializeField] private GameObject _visualQue;

    [Header("INK .json File")]
    [SerializeField] private TextAsset[] inkJson;

    private int index;
    private bool _playerInRange;
    public bool haveMorral = false;
    [SerializeField] private ChangeScenes changeScenes;
    [SerializeField] private string _itemTag = "Morral";

    private void Awake()
    {
        _playerInRange = false;
        _visualQue.SetActive(false);
        index = 0;
    }
    private void Update()
    {
        if (Game_Manager.instance.isGamePaused) return;
        if (Game_Manager.instance.InCombat) return;
        if (_playerInRange && !INK_Dialogue_Manager.instance._isDialogueRunning)
        {
            _visualQue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && index < inkJson.Length && haveMorral == true)
            {
                //iniciar dialogo
                changeScenes.reaadyToGO = true;
                INK_Dialogue_Manager.instance.EnterDialogueMode(inkJson[index]);
                index++;
            }
        }
        else
        {
            _visualQue.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (col.CompareTag(_itemTag))
            {
                //Touching Item
                haveMorral = true;
                itemWorld.DestroySelf();
            }
        }
        if (col.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}
