using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(RangedAttack))]
[RequireComponent(typeof(Damageable))]

public class PlayerController : MonoBehaviour, IDataPersistance
{
    [SerializeField] private Vector2 _lookDir;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private float _meleeAttackRate;
    [SerializeField] private RangedAttack _rangedAttack;
    [SerializeField] private float _rangedAttackRate;
    private InventorySimple inventory;
    [SerializeField] private UI_Inventory uiInventory;
    public Damageable Damageable => _damageable;
    private Damageable _damageable;


    private Movement _movement;
    private Vector2 _moveDir;
    private Animator _anim;
    private float _currMeleeTime;
    private float _currRangedTime;
    private bool _isAttacking;
    private bool _isUiInventoryActivate = false;
    private void Awake()
    {
        _currMeleeTime = 0;
        _currRangedTime = 0;
        _damageable = GetComponent<Damageable>();
        _damageable.onDie.AddListener(OnDieListener);
        inventory = new InventorySimple();
        uiInventory.SetInventory(inventory);
        uiInventory.SetPlayer(this);
        uiInventory.DeactivateUI();
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _anim = GetComponent<Animator>();
        if (!_meleeAttack) _meleeAttack = GetComponent<MeleeAttack>();
        _rangedAttack = GetComponent<RangedAttack>();
    }
    
    private void Update()
    {
        if (Game_Manager.instance.isGamePaused) return;
        _currMeleeTime += Time.deltaTime;
        _currRangedTime += Time.deltaTime;
        
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(hor, ver);
        _anim.SetFloat("AnimVelX", hor);
        _anim.SetFloat("AnimVelY", ver);
        _anim.SetBool("Attacking", false);

        if(Input.GetKeyDown(KeyCode.I))
        {
            _isUiInventoryActivate = !_isUiInventoryActivate;
            ActivateUI();
        }
        if (INK_Dialogue_Manager.instance._isDialogueRunning) return;
        if (_moveDir != Vector2.zero)
        {
            _lookDir = _moveDir;
        }
        
        if (!Game_Manager.instance.InCombat) return;
        if (Input.GetButtonDown("Fire1"))
        {
            MeleeAttack();
        }
        else
        {
            _isAttacking = false;
            _anim.SetBool("Attacking", false);
        }     
        
        if (Input.GetButtonDown("Fire2"))
        {
            RangedAttack();
        }
    }
    public void ActivateUI()
    {
        if(_isUiInventoryActivate == true)
        {
            uiInventory.ActivateUI();
        }
        if (_isUiInventoryActivate == false)
        {
            uiInventory.DeactivateUI();
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void FixedUpdate()
    {
        if (Game_Manager.instance.isGamePaused) return;
        if (_moveDir != Vector2.zero)
        {
            _anim.SetBool("Hit", false);
            _movement.Move(_moveDir.normalized);
        }
        else _anim.SetBool("Hit", false);
    }

    void OnDieListener()
    {
        //TODO
        //play die animation
        //restrict input
        //destroy or deactivate gameObject
        gameObject.SetActive(false);
    }
    void MeleeAttack()
    {
        if (_currMeleeTime >= _meleeAttackRate)
        {
            _isAttacking = true;
            _anim.SetBool("Attacking", true);
            _meleeAttack.Attack(_lookDir);
            _currMeleeTime = 0f;
        }
    }


    void RangedAttack()
    {
        if (_currRangedTime >= _rangedAttackRate)
        {
            _rangedAttack.Attack(_lookDir);
            _currRangedTime = 0f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //Touching Item
            inventory.AddItem(itemWorld.GetItem(),19);
            itemWorld.DestroySelf();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.white;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _lookDir);
        Gizmos.color = Color.blue;
        
        Vector2 center = (Vector2) transform.position + _lookDir * _meleeAttack.Range;
        Gizmos.DrawWireCube(center, _meleeAttack.Size);
    }

    public void LoadData(GameData data)
    {
        transform.position = data.playerPosition;
        _damageable.SetLife(data.playerCurrentLife);
        inventory.SetItemsList(data.itemList);
        uiInventory.RefreshInventoryItems();
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = transform.position;
        data.playerCurrentLife = _damageable.CurrentLife;
        data.itemList = new List<Item>(inventory.GetItemsList());

    }
}
