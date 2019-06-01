using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _bunnyAnim;
    private Animator _wingsAnim;
    [SerializeField] private ParticleSystem _dust;

    // Start is called before the first frame update
    void Awake()
    {
        _bunnyAnim = transform.GetChild(0).GetComponent<Animator>();
        _wingsAnim = transform.GetChild(1).GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(float speed)
    {
        _bunnyAnim.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void Jump(bool isJumping)
    {
        Debug.Log(isJumping);
        _bunnyAnim.SetBool("Jumping", isJumping);
    }

    public void Dust(Transform foot)
    {
        Instantiate(_dust, foot.position, Quaternion.identity);
    }

    public void Fly(bool isFlying) {
        _bunnyAnim.SetBool("Flying", isFlying);
    } 
}
