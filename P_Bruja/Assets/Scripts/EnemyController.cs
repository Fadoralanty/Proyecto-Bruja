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
    [SerializeField] private Collider2D[] myColliders;
    [SerializeField] private bool _isStunned;
    [SerializeField] private float distance;
    private Vector2 diff;
    private Rigidbody2D myRigidbody2D;
    private float _currMeleeTime;
    private Movement _movement;
    private Damageable _damageable;
    private MeleeAttack _meleeAttack;
    private Animator _anim;
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
        _movement.canMove = true;
    }

    private void Update()
    {
        _currMeleeTime += Time.deltaTime;
        diff = _target.position - transform.position;
        distance = diff.magnitude;
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
        if (_imDead == false)
        {
            myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

            if (distance <= _attackRange)
        {
            if (_imDead == false)
            {
                Attack();
            }
        }
        else
        {
            _anim.SetBool("Hit", false);
            _anim.SetBool("Attacking", false);
        }
        if (distance <= _detectionRange)
        {
            _movement.Move(diff.normalized);
            _anim.SetFloat("AnimVelX", diff.x);
            _anim.SetFloat("AnimVelY", diff.y);
        }
        else
        {
            _anim.SetBool("Hit", false);
            //_movement.canMove = false;
        }
    }

    void Attack()
    {
        if (_currMeleeTime >= _meleeAttackRate)
        {
            StopCoroutine(Wait(1f));
            StartCoroutine(Wait(1f));
            MeleeAttack(diff.normalized);
            _currMeleeTime = 0f;
        }
        else
        {
            _anim.SetBool("Hit", false);
        }
        
    }
    void MeleeAttack(Vector2 dir)
    {
        _anim.SetBool("Attacking", true);
        _meleeAttack.Attack(dir);
    }
    
    void OnDieListener()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        _anim.SetBool("Dead", true);
        _imDead = true;
        Game_Manager.instance.InCombat = false;
        foreach (var boxCollider2D in myColliders)
        {
            boxCollider2D.enabled = false;
        }
    }

    IEnumerator Wait(float time)
    {
        _movement.canMove = false;
        yield return new WaitForSeconds(time);
        _anim.SetBool("Hit", false);
        _movement.canMove = true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.cyan;
        Gizmos.DrawWireSphere(transform.position,_detectionRange);
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,_attackRange);
    }
}
