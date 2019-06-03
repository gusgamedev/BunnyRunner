using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private PlayerAnimation _anim;

    private bool _wasJumping = false;
    private bool _grounded = false;

    [SerializeField] private bool _canFly = false;
    [SerializeField] private GameObject _wings;
    [SerializeField] private float _wingsDuration = 4f;

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

    private void Start()
    {
        
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
        _wings.GetComponent<AudioSource>().Play();
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

    public void SpringJump(float jumpForceSpring)
    {
        if (_rigid.velocity.y < 0)
        {
            GetComponent<BetterJumping>().PauseBetterJump();
            Jump(jumpForceSpring);
        }
    }

    public void StartFly()
    {
        _canFly = true;
        _wings.SetActive(true);
        Invoke("CancelFly", _wingsDuration);
    }

    void CancelFly()
    {
        _canFly = false;
        _wings.SetActive(false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_foot.position, _radius);
    }

    void Die()
    {
        _canMove = false;
        CancelFly();
        _anim.Die();
        GetComponent<BetterJumping>().PauseBetterJump();
        Jump(15f);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (_canMove)
            if (collision.CompareTag("Die"))
                Die();
    }

}
