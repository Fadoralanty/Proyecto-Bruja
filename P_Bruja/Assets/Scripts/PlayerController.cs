using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _lookDir;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private float _meleeAttackRate;
    [SerializeField] private RangedAttack _rangedAttack;
    [SerializeField] private float _rangedAttackRate;
    private Movement _movement;
    private Vector2 _moveDir;
    private float _currMeleeTime;
    private float _currRangedTime;
    private bool _isAttacking;
    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Start()
    {
        _meleeAttack = GetComponent<MeleeAttack>();
        _rangedAttack = GetComponent<RangedAttack>();
        _currMeleeTime = 0;
        _currRangedTime = 0;
    }

    private void Update()
    {
        _currMeleeTime += Time.deltaTime;
        _currRangedTime += Time.deltaTime;
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(hor, ver);
        if (_moveDir != Vector2.zero) _lookDir = _moveDir;
        if (Input.GetButtonDown("Fire1"))
        {
            MeleeAttack();
        }
        else
        {
            _isAttacking = false;
        }     
        
        if (Input.GetButtonDown("Fire2"))
        {
            RangedAttack();
        }

    }

    void MeleeAttack()
    {
        if (_currMeleeTime >= _meleeAttackRate)
        {
            _isAttacking = true;
            _meleeAttack.Attack(_lookDir);
            _currMeleeTime = 0f;
        }
    }

    void RangedAttack()
    {
        if (_currRangedTime >= _rangedAttackRate)
        {
            _rangedAttack.Attack(transform.rotation);
            _currMeleeTime = 0f;
        }
    }
    
    private void FixedUpdate()
    {
        if (INK_Dialogue_Manager.instance._isDialogueRunning) return;
        _movement.Move(_moveDir.normalized);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + _lookDir);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + _lookDir, Vector2.one);
    }
}
