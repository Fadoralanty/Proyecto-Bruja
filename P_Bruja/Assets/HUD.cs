using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private List<GameObject> BattleHUD_Items;
    [SerializeField] private List<GameObject> moralityItems;
    
    void Start()
    {
        ToggleHUDItems(BattleHUD_Items,false);
        ToggleHUDItems(moralityItems,false);
    }

    void Update()
    {
        ToggleHUDItems(BattleHUD_Items,Game_Manager.instance.InCombat);
    }

    void ToggleHUDItems(List<GameObject> items, bool setActive)
    {
        foreach (var item in items)
        {
            item.SetActive(setActive);
        }
    }
}
