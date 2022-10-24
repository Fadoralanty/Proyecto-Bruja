using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySimple
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    public InventorySimple()
    {
        itemList = new List<Item>();
    }
    public void AddItem(Item item)
    {    
        itemList.Add(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public List<Item> GetItemsList()
    {
        return itemList;
    }
}
