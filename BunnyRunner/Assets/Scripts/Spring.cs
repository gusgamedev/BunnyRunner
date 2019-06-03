using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{

    private Animator _anim;
    [SerializeField] private float _jumpForce = 20f;


    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().SpringJump(_jumpForce);
            _anim.SetTrigger("Activate");            
        }
    }

    
}
