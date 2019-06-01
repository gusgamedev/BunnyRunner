using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;
    private bool _grounded = false;
    private bool _wasJumping = false;
    [SerializeField] private bool _canFly = false;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _foot;
    [SerializeField] private float _radius;
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

        if (Input.GetButtonDown("Jump") && _canFly)
            Fly();
        else if (Input.GetButtonDown("Jump") && _grounded)
            Jump(_jumpForce);

        _anim.Jump(!_grounded);
        
        if (!_grounded)
          _wasJumping = true;

        OnLand();
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

    private void Jump(float jumpForce)
    {   
        _rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce);
        
    }

    private void Fly()
    {   
        Jump(_jumpForce/1.5f);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(_foot.position, _radius, _layerMask);
    }

    void OnLand()
    {
        if (_wasJumping && _grounded)
        {
            _wasJumping = false;
            _anim.Dust(_foot);
        }
    }

    public void SpringJump()
    {
        if (_rigid.velocity.y < 0)
            Jump(_jumpForce * 1.75f);
    }

    void CancelFly()
    {
        _canFly = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_foot.position, _radius);
    }


}
