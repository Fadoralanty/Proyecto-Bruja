using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class ItemBase : MonoBehaviour, IItem
{
    [SerializeField] private InventoryItemData data;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2d;
    
    public InventoryItemData Data => data;
    public GameObject ItemObject => gameObject;
    public bool IsInHand { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2d = GetComponent<Collider2D>();

        _collider2d.isTrigger = true;
        
        if(data != null)
            SetItemData(data);
    }

    public void SetItemData(InventoryItemData newData)
    {
        data = newData;
        _spriteRenderer.sprite = data.Sprite;
    }

    public void SetIsInHand(bool input) => IsInHand = input;

    public virtual void UseItem() {}
}
