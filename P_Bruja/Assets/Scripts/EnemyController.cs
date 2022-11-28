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
    private Rigidbody2D myRigidbody2D;
    private float _currMeleeTime;
    private Movement _movement;
    private Damageable _damageable;
    private MeleeAttack _meleeAttack;
    private Animator _anim;
    private bool _isStunned;
    public bool _imDead;
    public Item item;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _meleeAttack = GetComponent<MeleeAttack>();
        _damageable = GetComponent<Damageable>();
        _anim = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myRigidbody2D.bodyType = RigidbodyType2D.Static;
        _damageable.onDie.AddListener(OnDieListener);
        _damageable.onLifeChange+=OnLifeChangeHandler;
        _currMeleeTime = 0f;
        _attackRange = _meleeAttack.Range;

    }

    private void OnLifeChangeHandler(float life)
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
    private void FixedUpdate()
    {
        if (Game_Manager.instance.isGameOver) return;
        if (Game_Manager.instance.isGamePaused) return;
        if (INK_Dialogue_Manager.instance._isDialogueRunning) return;
        if (_isStunned) return;
        if (!Game_Manager.instance.InCombat)
        {
            myRigidbody2D.bodyType = RigidbodyType2D.Static;
            return;
        }

        myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _currMeleeTime += Time.deltaTime;
        Vector2 diff = _target.position - transform.position;
        float distance = diff.magnitude;
        if (distance <= _detectionRange)
        {
            _anim.SetBool("Attacking", false);
            _movement.Move(diff.normalized);
            _anim.SetFloat("AnimVelX", diff.x);
            _anim.SetFloat("AnimVelY", diff.y);
            if (distance <= _attackRange)
            {
                if (_currMeleeTime >= _meleeAttackRate)
                {
                    StopCoroutine(Wait(1f));
                    StartCoroutine(Wait(1f));
                    _anim.SetBool("Attacking", true);
                    MeleeAttack(diff.normalized);
                    _currMeleeTime = 0f;
                }
                else _anim.SetBool("Hit", false);
            }
            else _anim.SetBool("Hit", false);
        }
        else
        {
            _anim.SetBool("Hit", false);
            _movement.Move(Vector2.zero);
        }
    }

    void MeleeAttack(Vector2 dir)
    {
        _meleeAttack.Attack(dir);
    }
    
    void OnDieListener()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        _anim.SetBool("Dead", true);
        _imDead = true;
        Game_Manager.instance.InCombat = false;
    }

    IEnumerator Wait(float time)
    {
        _movement.canMove = false;
        yield return new WaitForSeconds(time);
        _anim.SetBool("Hit", false);
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
