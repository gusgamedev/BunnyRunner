using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{

    private Animator _anim;


    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().SpringJump();
            _anim.SetTrigger("Jump");            
        }
    }

    
}
