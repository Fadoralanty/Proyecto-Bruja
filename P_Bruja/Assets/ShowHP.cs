using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHP : MonoBehaviour
{
    [SerializeField] private GameObject hpbar;

    private void Update()
    {
        hpbar.SetActive(Game_Manager.instance.InCombat);
    }
}
