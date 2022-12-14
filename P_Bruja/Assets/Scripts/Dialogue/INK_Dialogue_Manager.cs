using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class INK_Dialogue_Manager : MonoBehaviour
{
    [Header("Dialogue UI")] 
    [SerializeField] private GameObject _dialogueUI;

    [SerializeField] private TextMeshProUGUI _dialogueText;
    private Story _currStory;
    public bool _isDialogueRunning { get; private set; }

    [Header("Choices UI")] 
    [SerializeField] private GameObject[] _choices;

    private TextMeshProUGUI[] _choicesText;
    public UnityEvent onDialogueFinished;

    public static INK_Dialogue_Manager instance;
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
        _isDialogueRunning = false;
        _dialogueUI.SetActive(false);
        _choicesText = new TextMeshProUGUI[_choices.Length];
        int index = 0;
        foreach (GameObject choice in _choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //we only want to update if there is dialogue playing
        if (Game_Manager.instance.InCombat) return;
        if (!_isDialogueRunning)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E) && _currStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        _currStory = new Story(inkJson.text);
        _isDialogueRunning = true;
        _dialogueUI.SetActive(true);
        ContinueStory();
    }

    void ContinueStory()
    {
        if (_currStory.canContinue)
        {
            _dialogueText.text = _currStory.Continue();
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
        
    }
    
    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        if (_currStory.variablesState.GetVariableWithName("Combate")?.ToString()=="true")
        {
            Game_Manager.instance.InCombat = true;
        }
        _isDialogueRunning = false;
        _dialogueUI.SetActive(false);
        _dialogueText.text = "";
        onDialogueFinished.Invoke();
    }

    void DisplayChoices()
    {
        List<Choice> currChoices = _currStory.currentChoices;
        if (currChoices.Count> _choices.Length)
        {
            Debug.LogError("More Choices were given than the UI can support. Number of choices given: " 
                           + currChoices.Count);
        }
        
        //enable and initialize the choices up to the amount of choices for this line of dialogue
        int index = 0;
        foreach (Choice choice in currChoices)
        {
            _choices[index].gameObject.SetActive(true);
            _choicesText[index].text = choice.text;
            index++;
        }
        //hid remaining choices
        for (int i = index; i < _choices.Length; i++)
        {
            _choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    public void MakeChoice(int choiceIndex)
    {
        _currStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        int mor =int.Parse(_currStory.variablesState.GetVariableWithName("Moralidad")?.ToString());
        Game_Manager.instance.MoralityPoints(mor);
    }
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_choices[0].gameObject);
        
    }
}
