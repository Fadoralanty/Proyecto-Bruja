using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class PlayerController : MonoBehaviour
{
    private Movement _movement;
    private Vector2 _moveDir;
    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(hor, ver);
        
    }

    private void FixedUpdate()
    {
        _movement.Move(_moveDir.normalized);
    }
}
