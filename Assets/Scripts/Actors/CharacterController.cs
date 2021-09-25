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

    protected Vector2 Velocity;
    protected bool Grounded;
    protected bool Current = false;

    protected bool facingRight = true;


    private void Start()
    {
        GameController.Instance.AddCharacter(character, this);
        GameController.CharacterSwitchListener += OnCharacterSwitch;
    }

    private void OnDestroy()
    {
        GameController.CharacterSwitchListener -= OnCharacterSwitch;
    }

    protected virtual void Update()
    {
        if (!Current) return;
        Velocity.x = Input.GetAxis("Horizontal") * walkSpeed;
        if (Velocity.x != 0) facingRight = Velocity.x > 0;
        if (Grounded && Input.GetButtonDown("Jump")) Jump();
        //if (Input.GetButtonDown("Switch")) GameController.Instance.SwitchCharacter(GameController.Character.Cheese);
        CheckCharacterSwitch();
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Velocity.x, rb2D.velocity.y);
    }

    protected void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
    }

    private void CheckCharacterSwitch()
    {
        if(Input.GetKeyDown(KeyCode.Z))GameController.Instance.SwitchCharacter(GameController.Character.Patty);
        if(Input.GetKeyDown(KeyCode.X))GameController.Instance.SwitchCharacter(GameController.Character.Cheese);
        if(Input.GetKeyDown(KeyCode.C))GameController.Instance.SwitchCharacter(GameController.Character.Lettuce);
        if(Input.GetKeyDown(KeyCode.V))GameController.Instance.SwitchCharacter(GameController.Character.Tomato);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.IsTouching(groundCheck))
        {
            Grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            Grounded = false;
        }
    }

    private void OnCharacterSwitch(GameController.Character c)
    {
        Current = character == c;
        rb2D.constraints = character == c ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
}
