using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItem : MonoBehaviour
{
    [SerializeField] private GameObject playerHand;
    [SerializeField] private float pickUpRadius;
    [SerializeField] private float dropRadius;
    
    private InventorySystem _inventorySystem;

    private InventorySlot _assignedInventorySlot;

    public IItem ItemInHand { get; private set; }
    public bool HasItemInHand { get; private set; }


    private void Awake()
    {
        _inventorySystem = transform.GetComponent<InventoryHolder>().InventorySystem;
        _assignedInventorySlot = new InventorySlot(); //The hand works as a InventorySlot, just to grab items
    }
    
    private bool CheckForNearbyItems(out IItem itemToPickUp)
    {
        var nearbyItem = Physics2D.OverlapCircleAll(transform.position, pickUpRadius);
        foreach (var item in nearbyItem)
        {
            if (item.TryGetComponent(out itemToPickUp))
                return true;
        }

        itemToPickUp = null;
        return false;
    }

    private void SetItemInHand(IItem item)
    {
        _assignedInventorySlot.UpdateInventorySlot(item, 1);
        ItemInHand = _assignedInventorySlot.ItemData;
        ItemInHand.SetIsInHand(true);
        ItemInHand.ItemObject.transform.SetParent(playerHand.transform);
        ItemInHand.ItemObject.transform.position = playerHand.transform.position;
        ItemInHand.ItemObject.SetActive(true);
        HasItemInHand = true;
    }

    private void ClearHand()
    {
        if (!HasItemInHand) return;

        ItemInHand = null;
        HasItemInHand = false;
        _assignedInventorySlot.ClearSlot();
    }

    private void ClearSlot()
    {
        _assignedInventorySlot = new InventorySlot();
        ItemInHand = null;
        HasItemInHand = false;
    }

    public void AddItemToInventory()
    {
        if (!CheckForNearbyItems(out IItem itemToAdd)) return;

        if (itemToAdd.IsInHand) return;

        if (_inventorySystem.AddToInventory(itemToAdd))
        {
            Debug.Log($"{itemToAdd.Data.DisplayName} Collected"); 
        }
    }

    public void GrabItem()
    {
        if(!CheckForNearbyItems(out IItem itemToGrab)) return;

        if (HasItemInHand) return;
        
        SetItemInHand(itemToGrab);
    }

    public void DropItem()
    {
        if (!HasItemInHand) return;

        ItemInHand.SetIsInHand(false);
        ItemInHand.ItemObject.transform.position = transform.position + transform.right * dropRadius;
        ItemInHand.ItemObject.transform.parent = null;
        ClearHand();
        ClearSlot();
    }

    public void UseItem()
    {
        _assignedInventorySlot.ItemData?.UseItem();
    }

    public void EquipItem(int input)
    {
        var slotToGet = _inventorySystem.GetSlotByIndex(input);

        if (slotToGet.ItemData == null) return;
        
        if (_assignedInventorySlot.ItemData == slotToGet.ItemData)
        {
            _inventorySystem.MoveItemToBox(_assignedInventorySlot.ItemData);
            ClearSlot();
            return;
        }

        if (HasItemInHand)
            if (!_assignedInventorySlot.ItemData.Data.CanAddToInventory)
                DropItem();

        if (_assignedInventorySlot.ItemData != null)
        {
            _inventorySystem.MoveItemToBox(_assignedInventorySlot.ItemData);
        }

        _assignedInventorySlot = slotToGet;

        SetItemInHand(_assignedInventorySlot.ItemData);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
        /*Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, dropRadius);*/
    }
}
