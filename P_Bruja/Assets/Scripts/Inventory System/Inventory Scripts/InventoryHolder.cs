using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] protected InventorySystem inventorySystem;
    [SerializeField] private Transform itemBox;

    private const int InventorySize = 9;
    
    public InventorySystem InventorySystem => inventorySystem;
    
    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventorySystem = new InventorySystem(InventorySize, itemBox);
    }
}
