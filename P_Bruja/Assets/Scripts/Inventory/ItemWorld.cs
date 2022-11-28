using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private GameObject itemGot;
    public static ItemWorld SpawnItemWorld(Vector3 position,Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.itemWorld, position, Quaternion.identity);
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    public static ItemWorld DropItem(Vector3 dropPosition,Item item)
    {
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + new Vector3(1,0,0), item);
        //itemWorld.GetComponent<Rigidbody2D>().AddForce(dropPosition + new Vector3(2, 0, 0), ForceMode2D.Impulse);
        return itemWorld;
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        gameObject.tag = item.GetTag();
    }
    public Item GetItem()
    {
        return item;
    }
    public void DestroySelf()
    {
         ItemObtained i =Instantiate(itemGot,transform.position,transform.rotation).GetComponent<ItemObtained>();
         i.text.text = item.itemType.ToString() + " obtained";
         switch (item.itemType)
         {
             case Item.ItemType.Morral:
                 AudioManager.instance.play("pickup bolso");
                 break;
             case Item.ItemType.Ganzua:
                 AudioManager.instance.play("Ganzua pick up");
                 break;
             case Item.ItemType.Key:
                 AudioManager.instance.play("llave pickup");
                 break;
             case Item.ItemType.BackPackBones:
                 AudioManager.instance.play("pickup huesos 1");
                 break;
             case Item.ItemType.BackPackBook:
                 AudioManager.instance.play("pickup bolso");
                 break;
         }
        Destroy(gameObject);
    }
}
