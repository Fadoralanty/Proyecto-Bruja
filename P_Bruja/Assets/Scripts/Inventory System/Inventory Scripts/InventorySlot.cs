using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private int stackSize;

    public IItem ItemData { get; private set; }
    public int StackSize => stackSize;

    public InventorySlot(IItem source, int amount)
    {
        ItemData = source;
        stackSize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        ItemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot)
    {
        if(ItemData == invSlot.ItemData) AddToStack(invSlot.stackSize);
        else
        {
            ItemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(IItem data, int amount)
    {
        ItemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountReamaining)
    {
        amountReamaining = ItemData.Data.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }
    
    public bool RoomLeftInStack(int amountToAdd)
    {
        return (stackSize + amountToAdd) <= ItemData.Data.MaxStackSize;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
