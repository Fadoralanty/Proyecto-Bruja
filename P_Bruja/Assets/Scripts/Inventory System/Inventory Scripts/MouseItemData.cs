using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseItemData : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private Text itemCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    //Testing
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float dropDistance;

    public Image ItemSprite => itemSprite;
    public Text ItemCount => itemCount;
    public InventorySlot AssignedInventorySlot => assignedInventorySlot;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    private void Update()
    {
        if (assignedInventorySlot.ItemData != null)
        {
            transform.position = Input.mousePosition;
        }
    }
    

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        assignedInventorySlot.AssignItem(invSlot);
        itemSprite.sprite = invSlot.ItemData.Data.Icon;
        itemSprite.color = Color.white;
        itemCount.text = invSlot.StackSize > 1 ? invSlot.StackSize.ToString() : "";
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> restuls = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, restuls);
        return restuls.Count > 0;
    }
}
