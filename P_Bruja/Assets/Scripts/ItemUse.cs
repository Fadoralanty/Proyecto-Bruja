using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] private string _itemTag = "Morral";
    private bool _isUse;
    private Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
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
            if (col.CompareTag(_itemTag))
            {
                //Touching Item
                _isUse = true;
                itemWorld.DestroySelf();
            }
        }
    }
}
