using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Morral
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Morral: return ItemAssets.Instance.morralSprite;
        }
    }

    public bool IsStackeble()
    {
        switch (itemType)
        {
            default:
            case ItemType.Morral:
                return false;
        }
    }
}