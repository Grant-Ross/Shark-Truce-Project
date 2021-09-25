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
    private bool current = false;


    private void Start()
    {
        GameController.Instance.AddCharacter(character, this);
        GameController.characterSwitchListener += OnCharacterSwitch;
    }

    private void OnDestroy()
    {
        GameController.characterSwitchListener -= OnCharacterSwitch;
    }

    private void Update()
    {
        if (!current) return;
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

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") && other.IsTouching(groundCheck))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            grounded = false;
        }
    }

    private void OnCharacterSwitch(GameController.Character c)
    {
        current = character == c;
        rb2D.constraints = character == c ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
}
