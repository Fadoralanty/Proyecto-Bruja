using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private bool _hasKnockback;
    [SerializeField] private float _knockback = 2;
    [SerializeField] private float _knockbackTime = 0.2f;
    private Vector2 center;
    private Vector2 attackDir;
    public void Attack(Vector2 dir)
    {
        attackDir = (Vector2)transform.position + dir;
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(attackDir,  Vector2.one, 0,_targetMask);
        foreach (Collider2D hit in hitTargets)
        {
            hit.GetComponent<Damageable>().GetDamage(_damage);
            if (_hasKnockback)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                Vector2 diff = rb.transform.position - transform.position;
                rb.AddForce(diff.normalized * _knockback, ForceMode2D.Impulse);
                StartCoroutine(StopKnockBack(rb));
            }
        }
    }

    IEnumerator StopKnockBack(Rigidbody2D rb)
    {
        yield return new WaitForSeconds(_knockbackTime);
        rb.velocity = Vector2.zero;
    }

}
