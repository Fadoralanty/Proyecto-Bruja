using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//https://www.youtube.com/watch?v=_nRzoTzeyxU&list=WL&index=22&t=4s
public class Dialogue_Manager : MonoBehaviour
{
    private Queue<string> Sentences;
    public static Dialogue_Manager instance;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueCanvas;
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
    }
    private void Start()
    {
        Sentences = new Queue<string>();
        dialogueCanvas.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueCanvas.SetActive(true);
        nameText.text = dialogue._name;
        Sentences.Clear();
        foreach (string sentence in dialogue._sentences)
        {
            Sentences.Enqueue(sentence);
        }
        string firstSentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(firstSentence));
        
        
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);//delay entre cada letra
        }
    }
    public void DisplayNextSentence()
    {
        if (Sentences.Count==0)
        {
             EndDialogue();
             return;
        }

        string sentence = Sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
    }

    public void CloseDialogue()
    {
        dialogueCanvas.SetActive(false);
    }
}
