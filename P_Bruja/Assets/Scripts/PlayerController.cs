using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class PlayerController : MonoBehaviour
{
    private Movement _movement;
    private Vector2 _moveDir;
    private Animator _anim;
    private InventorySimple inventory;
    [SerializeField] private UI_Inventory uiInventory;
    private bool uiInventoryActive = true;
    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _anim = GetComponent<Animator>();

        inventory = new InventorySimple();
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(inventory);  
        uiInventory.DeactivateUI();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            uiInventoryActive = !uiInventoryActive;
            ActivationUIInventory();
        }
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(hor, ver);
        _anim.SetFloat("AnimVelX", hor);
        _anim.SetFloat("AnimVelY", ver);
    }
    private void ActivationUIInventory()
    {
        if (uiInventoryActive == false)
        {
            uiInventory.ActivateUI();
        }
        if (uiInventoryActive == true)
        {
            uiInventory.DeactivateUI();
        }
    }
    private void FixedUpdate()
    {
        _movement.Move(_moveDir.normalized);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem(), 19);
            itemWorld.DestroySelf();
        }
    }
}
