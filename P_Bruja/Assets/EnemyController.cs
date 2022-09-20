using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Movement _movement;
    private Damageable _damageable;
    private void Start()
    {
        _movement = GetComponent<Movement>();
        _damageable = GetComponent<Damageable>();
        _damageable.onDie.AddListener(OnDieListener);
    }
    
    

    void OnDieListener()
    {
        Destroy(gameObject);
    }
}
