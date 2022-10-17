using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(MeleeAttack))]
[RequireComponent(typeof(RangedAttack))]
[RequireComponent(typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _lookDir;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private float _meleeAttackRate;
    [SerializeField] private RangedAttack _rangedAttack;
    [SerializeField] private float _rangedAttackRate;
    private Damageable _damageable;
    private Movement _movement;
    private Vector2 _moveDir;
    private Animator _anim;
    private float _currMeleeTime;
    private float _currRangedTime;
    private bool _isAttacking;
    private void Awake()
    {
        _currMeleeTime = 0;
        _currRangedTime = 0;
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _anim = GetComponent<Animator>();
        _meleeAttack = GetComponent<MeleeAttack>();
        _rangedAttack = GetComponent<RangedAttack>();
        _damageable = GetComponent<Damageable>();
        _damageable.onDie.AddListener(OnDieListener);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _lookDir);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + _lookDir, Vector2.one);
    }
}
