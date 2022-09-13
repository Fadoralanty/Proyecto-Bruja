using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _knockback;
    [SerializeField] private Collider2D _meleeAttackHitBox;
    
    private void Start()
    {
        _meleeAttackHitBox = GetComponent<BoxCollider2D>();
        _meleeAttackHitBox.enabled = false;
    }
    

    public IEnumerator Attack(Vector2 dir,float time)
    {
        transform.position = dir;
        _meleeAttackHitBox.enabled = true;
        yield return new WaitForSeconds(time);
        _meleeAttackHitBox.enabled = false;
        transform.position=Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))//TODO cambiar a la tag del enemy/enemies
        {
            //TODO HACER DAÑO
        }
    }
}
