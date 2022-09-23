using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool canMove;
    [SerializeField] float _Speed;
    Rigidbody2D _rb;

    private void Awake()
    {
        canMove = true;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 dir)
    {
        if (!canMove) return;
        transform.position += (Vector3)( _Speed * Time.deltaTime * dir);
    }
    
    
}
