using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class PlayerController : MonoBehaviour
{
    private Movement _movement;
    private Vector2 _moveDir;
    [SerializeField] private Vector2 _lookDir;
    private MeleeAttack _meleeAttack;
    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Start()
    {
        _meleeAttack = GetComponentInChildren<MeleeAttack>();
    }

    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(hor, ver);
        if (_moveDir != Vector2.zero) _lookDir = _moveDir;
        if (Input.GetButtonDown("Fire1"))
        {
            MeleeAttack();
        }

    }

    void MeleeAttack()
    {
        StartCoroutine(_meleeAttack.Attack(_lookDir, 0.2f));
    }
    
    private void FixedUpdate()
    {
        _movement.Move(_moveDir.normalized);
    }
}
