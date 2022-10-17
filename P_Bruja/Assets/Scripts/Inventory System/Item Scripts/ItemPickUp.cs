using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private float pickUpRadius = 1;
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int amount = 1;

    private CircleCollider2D _myCollider;
    private SpriteRenderer _spriteRenderer;
    
    public InventoryItemData ItemData => itemData;
    public int Amount => amount;

    private void Awake()
    {
        _myCollider = GetComponent<CircleCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _myCollider.isTrigger = true;
        _myCollider.radius = pickUpRadius;
        
        if(itemData != null)
            SetItemData(ItemData);
    }

    public void SetItemData(InventoryItemData newData)
    {
        itemData = newData;
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = itemData.Sprite;
    }
}
