﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;
    private bool _grounded = false;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _foot;

    [SerializeField] private bool _canMove = false;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        _grounded = GroundCheck();
        Movement();
        OnLand();
        Jump();
    }
    
    private void Movement()
    {
        if (_canMove)
        {
            _rigid.velocity = new Vector2(_speed, _rigid.velocity.y);            

        } else
        {
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        }

        _anim.Walk(_rigid.velocity.x);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            
        }

        _anim.Jump(!_grounded);
    }

    private bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, _layerMask);
        return (hit.collider != null);
    }

    void OnLand()
    {
        if (_rigid.velocity.y < 0 && _grounded)
            _anim.Dust(_foot);
    }

    
}