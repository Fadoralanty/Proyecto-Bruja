using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=vY0Sk93YUhA&list=WL&index=19
public class INK_Dialogue_Trigger : MonoBehaviour
{ //TODO fixear problemas con input
    [Header("Visual Que")] 
    [SerializeField] private GameObject _visualQue;
    
    [Header("INK .json File")] 
    [SerializeField] private TextAsset inkJson;
    
    private bool _playerInRange;
    private void Awake()
    {
        _playerInRange = false;
        _visualQue.SetActive(false);
    }

    private void Update()
    {
        if (Game_Manager.instance.isGamePaused) return;
        if (_playerInRange && !INK_Dialogue_Manager.instance._isDialogueRunning)
        {
            _visualQue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                //iniciar dialogo
                INK_Dialogue_Manager.instance.EnterDialogueMode(inkJson); 
            }
        }
        else
        {
            _visualQue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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
