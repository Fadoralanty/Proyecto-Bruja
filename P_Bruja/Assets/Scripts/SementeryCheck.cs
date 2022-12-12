using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SementeryCheck : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    private Collider2D _collider;
    private int index;

    [Header("INK .json File")]
    [SerializeField] private TextAsset[] inkJson;

    private void Awake()
    {
        index = 0;
    }
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _enemy.SetActive(true);
            _collider.enabled = false;
            Game_Manager.instance.InCombat = false;
            if(index < inkJson.Length)
            {
                INK_Dialogue_Manager.instance.EnterDialogueMode(inkJson[index]);
                index++;
            }
        }
    }
}
