using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        Morral,
        Ganzua,
        Key,
        BackPackBones,
        BackPackBook
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Morral: return ItemAssets.Instance.morralSprite;
            case ItemType.Ganzua: return ItemAssets.Instance.ganzuaSprite;
            case ItemType.Key: return ItemAssets.Instance.keySprite;
            case ItemType.BackPackBones: return ItemAssets.Instance.backpackBoneSprite;
            case ItemType.BackPackBook: return ItemAssets.Instance.backpackBookSprite;
        }
    }
    public string GetTag()
    {
        switch (itemType)
        {
            default:
            case ItemType.Morral: return ItemAssets.Instance.tagMorral;
            case ItemType.Ganzua: return ItemAssets.Instance.tagGanzua;
            case ItemType.Key: return ItemAssets.Instance.tagKey;
            case ItemType.BackPackBones: return ItemAssets.Instance.tagBone;
            case ItemType.BackPackBook: return ItemAssets.Instance.tagBook;
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