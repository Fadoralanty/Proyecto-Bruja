using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=_nRzoTzeyxU&list=WL&index=22&t=4s
public class NpcDialogueTrigger : MonoBehaviour
{
    public Dialogue _dialogue;
    private bool isPlayerInRange;
    private bool isDialogueStarted;
    private bool isDialogueFinished;

    public GameObject speechBubbleSprite;
    

    public void TriggerDialogue()
    {
        Dialogue_Manager.instance.StartDialogue(_dialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isDialogueStarted)
            {
                Dialogue_Manager.instance.DisplayNextSentence();
            }
            if (isPlayerInRange && !isDialogueStarted)
            {
                TriggerDialogue();
                isDialogueStarted = true;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            isPlayerInRange = true;

        }
        speechBubbleSprite.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            isDialogueStarted = false; // Permite reiniciar el dialogo cuando te alejas
            Dialogue_Manager.instance.CloseDialogue(); //Cierra el dialogo cuando te alejas
        }
        speechBubbleSprite.SetActive(false);
    }
    
}
