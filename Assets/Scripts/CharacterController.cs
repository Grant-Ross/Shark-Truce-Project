using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2D = null;
    [SerializeField] private Collider2D groundCheck = null;

    public float walkSpeed;
    public float jumpPower;

    private Vector2 _velocity;
    private bool _grounded;


    private void Update()
    {
        _velocity.x = Input.GetAxis("Horizontal") * walkSpeed;
        if (_grounded && Input.GetButtonDown("Jump")) rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(_velocity.x, rb2D.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.otherCollider == groundCheck)
        {
            _grounded = true;

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.otherCollider == groundCheck)
        {
            _grounded = false;

        }
    }
}
