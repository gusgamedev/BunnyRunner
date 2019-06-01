using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool _isFalling = false;
    float _speed = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Invoke("Fall", 0.8f);
    }

    private void Update()
    {
        if (_isFalling)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            _speed++;
        }
    }

    void Fall()
    {
        _isFalling = true;
    }

}
