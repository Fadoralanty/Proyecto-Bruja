using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChoose : MonoBehaviour
{
    [SerializeField] private EnemyController _enemy;
    private int index;

    [Header("INK .json File")]
    [SerializeField] private TextAsset[] inkJson;
    private void Awake()
    {
        index = 0;
    }

    void Update()
    {
        if (_enemy._imDead == true && index < inkJson.Length)
        {
            INK_Dialogue_Manager.instance.EnterDialogueMode(inkJson[index]);
            index++;
        }

    }
}
