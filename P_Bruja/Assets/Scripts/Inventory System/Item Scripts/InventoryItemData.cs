using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string displayName;
    [SerializeField] [TextArea(4, 4)] private string description;
    [SerializeField] private bool canAddToInventory = true;
    [Range(0.1f, 1)][SerializeField] private float slowDownMultiplier = 1;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int maxStackSize = 1; //In the case that we can store like 3 bananas in only one item slot, if not I'll delete it
    [SerializeField] private GameObject itemPrefab;
    

    public int ID => id;
    public string DisplayName => displayName;
    public string Description => description;
    public bool CanAddToInventory => canAddToInventory;
    public float SlowDownMultiplier => slowDownMultiplier;
    public Sprite Icon => icon;
    public Sprite Sprite => sprite;
    public int MaxStackSize => maxStackSize;
    public GameObject ItemPrefab => itemPrefab;

}
