using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected GameController.Character character;
    [SerializeField] protected Rigidbody2D rb2D = null;
    [SerializeField] protected Collider2D groundCheck = null;

    public float walkSpeed;
    public float jumpPower;

    public Vector2 velocity;
    public bool grounded;


    private void Awake()
    {
        GameController.Instance.AddCharacter(character, this);
    }

    private void Update()
    {
        velocity.x = Input.GetAxis("Horizontal") * walkSpeed;
        if (grounded && Input.GetButtonDown("Jump")) rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
        //if (Input.GetButtonDown("Switch")) GameController.Instance.SwitchCharacter(GameController.Character.Cheese);
        if(Input.GetKeyDown(KeyCode.Z))GameController.Instance.SwitchCharacter(GameController.Character.Patty);
        if(Input.GetKeyDown(KeyCode.X))GameController.Instance.SwitchCharacter(GameController.Character.Cheese);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(velocity.x, rb2D.velocity.y);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.otherCollider == groundCheck)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.otherCollider == groundCheck)
        {
            grounded = false;
        }
    }
}
