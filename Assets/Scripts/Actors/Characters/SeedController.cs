using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float speed;

    private const float DeathTime = 1f;
    private float _timer;
    
    private void Start()
    {
        rb2D.velocity = transform.up * speed;
        _timer = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground")) Kill();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= DeathTime) Kill();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }
}
