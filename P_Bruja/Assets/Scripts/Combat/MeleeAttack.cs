using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    public float Range => _range;
    [SerializeField] private float _range = 1;
    public Vector2 Size => _size;
    [SerializeField] private Vector2 _size = Vector2.one;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private bool _hasKnockback;
    [SerializeField] private float _knockback = 2;
    [SerializeField] private float _knockbackTime = 0.2f;
    private Vector2 center;
    public Vector2 attackDir;
    public void Attack(Vector2 dir)
    {
        attackDir = (Vector2)transform.position + dir * _range;
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll( attackDir,  _size, 0,_targetMask);
        Debug.DrawLine(transform.position,attackDir*_range);
        foreach (Collider2D hit in hitTargets)
        {
            hit.GetComponent<Damageable>()?.GetDamage(_damage);
            if (_hasKnockback)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                // rb.isKinematic = false;
                Vector2 diff = rb.transform.position - transform.position;
                diff = diff.normalized * _knockback;
                rb.AddForce(diff, ForceMode2D.Impulse);
                // rb.isKinematic = true;
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
