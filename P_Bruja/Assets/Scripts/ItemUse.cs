using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] private string _item1Tag = "Morral";
    [SerializeField] private string _item2Tag = "Morral";
    [SerializeField] private Sprite _newSprite;
    private bool _isUse;
    private Collider2D collider2D;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (_isUse == true)
        {
            collider2D.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        ItemWorld itemWorld = col.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            if (col.CompareTag(_item1Tag))
            {
                //Touching Item
                _isUse = true;
                Game_Manager.instance.MoralityPoints(-25);
                _spriteRenderer.sprite = _newSprite;
                itemWorld.DestroySelf();
            }
            if (col.CompareTag(_item2Tag))
            {
                //Touching Item
                _isUse = true;
                _spriteRenderer.sprite = _newSprite;
                itemWorld.DestroySelf();
            }
        }
    }
}
