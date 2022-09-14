using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private LayerMask _targetMask;
    private Vector2 center;
    private Vector2 attackDir;
    public IEnumerator Attack(Vector2 dir,float time)
    {
        attackDir = (Vector2)transform.position + dir;
        Collider2D[] hitTargets = Physics2D.OverlapBoxAll(attackDir,  Vector2.one, 0,_targetMask);
        foreach (Collider2D hit in hitTargets)
        {
            hit.GetComponent<Damageable>().GetDamage(_damage);
        }
        yield return new WaitForSeconds(time);//Attack rate 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(attackDir,Vector3.one );
    }
}
