using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    public InventoryItemData Data { get; }
    public GameObject ItemObject { get; }
    public bool IsInHand { get; }
    public void SetItemData(InventoryItemData newData);
    public void SetIsInHand(bool input);
    public void UseItem();
}
