using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private InventorySimple inventory;
    [SerializeField] Transform itemSlotContainer;
    [SerializeField] Transform itemSlotTemplate;
    private PlayerController player;

    private void Awake()
    {
        
    }
    public void SetPlayer (PlayerController player)
    {
        this.player = player;
    }
    public void SetInventory(InventorySimple inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
            int x = 0;
            int y = 0;
            float itemSlotCellSize = 125f;
        foreach (Item item in inventory.GetItemsList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => { 
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => {
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), item);
            };
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Sprite").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
            if(x > 4)
            {
                x = 0;
                y--;
            }
        }
    }
}
