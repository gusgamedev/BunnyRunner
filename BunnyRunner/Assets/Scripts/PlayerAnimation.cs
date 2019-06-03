using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _bunnyAnim; 
    [SerializeField] private ParticleSystem _dust;

    // Start is called before the first frame update
    void Awake()
    {    
        _bunnyAnim = transform.GetChild(0).GetComponent<Animator>();        
    }

    
    public void Walk(float speed)
    {
        _bunnyAnim.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void Jump(bool isJumping)
    {  
        _bunnyAnim.SetBool("Jumping", isJumping);
    }

    public void Dust(Transform foot)
    {  
        Instantiate(_dust, foot.position, Quaternion.identity);
    }

    public void Fly(bool isFlying) {
        _bunnyAnim.SetBool("Flying", isFlying);
    } 

    public void Die()
    {
        _bunnyAnim.SetTrigger("Die");
    }

    
}
