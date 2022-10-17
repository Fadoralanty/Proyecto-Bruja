using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public Transform ItemBox { get; private set; }
    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size, Transform itemBox)
    {
        inventorySlots = new List<InventorySlot>(size);
        this.ItemBox = itemBox;

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(IItem itemToAdd, int amountToAdd = 1)
    {
        if (!itemToAdd.Data.CanAddToInventory) return false;

        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot))
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            MoveItemToBox(itemToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(IItem itemToAdd, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();//Check System.Linq
        return invSlot != null;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot != null;
    }

    public void MoveItemToBox(IItem itemToMove)
    {
        itemToMove.ItemObject.SetActive(false);
        var itemTrans = itemToMove.ItemObject.transform;
        itemTrans.SetParent(ItemBox);
        itemTrans.transform.position = ItemBox.position;
        
    }

    public InventorySlot GetSlotByIndex(int index)
    {
        return InventorySlots[index];
    }

}
