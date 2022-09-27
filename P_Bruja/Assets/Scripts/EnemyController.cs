using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _detectionRange; 
    [SerializeField] private float _attackRange; 
    [SerializeField] private float _meleeAttackRate;
    private float _currMeleeTime;
    private Movement _movement;
    private Damageable _damageable;
    private MeleeAttack _meleeAttack;
    private bool _isStunned;
    
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _meleeAttack = GetComponent<MeleeAttack>();
        _damageable = GetComponent<Damageable>();
        _damageable.onDie.AddListener(OnDieListener);
        _damageable.onLifeChange+=OnLifeChangeHandler;
        _currMeleeTime = 0f;
    }

    void OnLifeChangeHandler(float life)
    {
        StopAllCoroutines();
        StartCoroutine(Stun(1f));
    }

    IEnumerator Stun(float time)
    {
        _isStunned = true;
        yield return new WaitForSeconds(time);
        _isStunned = false;
    }
    private void Update()
    {
        if (_isStunned) return;
        _currMeleeTime += Time.deltaTime;
        Vector2 diff = _target.position - transform.position;
        float distance = diff.magnitude;
        if (distance <= _detectionRange)
        {
            _movement.Move(diff.normalized);
            if (distance <= _attackRange)
            {
                if (_currMeleeTime >= _meleeAttackRate)
                {
                    StopCoroutine(Wait(1f));
                    StartCoroutine(Wait(1f));
                    MeleeAttack(diff.normalized);
                    _currMeleeTime = 0f;
                }
            }
        }
        else
        {
            _movement.Move(Vector2.zero);
        }
    }

    void MeleeAttack(Vector2 dir)
    {
        _meleeAttack.Attack(dir);
    }
    
    void OnDieListener()
    {
        Destroy(gameObject);
    }

    IEnumerator Wait(float time)
    {
        _movement.canMove = false;
        yield return new WaitForSeconds(time);
        _movement.canMove = true;
    }
    
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(transform.position, _target.position);
    //     Gizmos.DrawWireSphere(transform.position, _attackRange);
    //     Vector2 diff = _target.position - transform.position;
    //     Gizmos.DrawWireCube((Vector2)transform.position + diff.normalized, Vector2.one);
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireSphere(transform.position, _detectionRange);
    // }
}