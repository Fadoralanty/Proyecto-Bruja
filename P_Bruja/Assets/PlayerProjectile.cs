using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
[RequireComponent(typeof(Movement))]
public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private float _damage;
    
    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void FixedUpdate()
    {
        _movement.Move(Vector2.right);
        Destroy(gameObject,1f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Damageable>().GetDamage(_damage);
            Destroy(gameObject);
        }
    }
}
