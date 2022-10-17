using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData mouseInventoryItem;
    
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    
    public InventorySystem InventorySystem => InventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {
        
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in slotDictionary)
        {
            if (slot.Value == updatedSlot) // Slot Value
            {
                slot.Key.UpdateUISlot(updatedSlot);//UI Representation of the value
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        //Clicked slot has an item, mouse doesn't = pick up the item
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && 
            mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        //Clicked slot doesn't have an item, mouse does = place the mouse item into the slot
        else if (clickedUISlot.AssignedInventorySlot.ItemData == null &&
            mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();
            
            mouseInventoryItem.ClearSlot();
        }

        else if (clickedUISlot.AssignedInventorySlot.ItemData != mouseInventoryItem.AssignedInventorySlot.ItemData)
        {
            SwapSlots(clickedUISlot);
        }
    }

    private void SwapSlots(InventorySlot_UI clickedSlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData,
            mouseInventoryItem.AssignedInventorySlot.StackSize);
        
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedSlot.AssignedInventorySlot);

        clickedSlot.ClearSlot();
        clickedSlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedSlot.UpdateUISlot();
    }
}
