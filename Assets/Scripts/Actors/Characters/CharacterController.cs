using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] protected GameController.Character character;
    [SerializeField] protected Rigidbody2D rb2D = null;
    [SerializeField] protected Collider2D groundCheck = null;
    [SerializeField] protected Animator animator;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected AudioClip jump;
    [SerializeField] protected AudioClip land;

    private const float walkSpeed = 10;
    private  const float jumpPower = 18;

    protected Vector2 Velocity;
    protected bool Grounded;
    protected bool Current = false;

    protected bool facingRight = true;

    public enum State
    {
        Idle, Walking, Jumping, Peak, Falling, Landing,
    }

    private bool _disabled = false;

    private State _currentState;
    protected State CurrentState
    {
        get => _currentState;
        set
        {
            if (_currentState == value) return;
            _currentState = value;
            if (value == State.Landing) _landTimer = LandTime;
            stateChanged = true;
        }
    }

    private bool stateChanged = false;
    private float _landTimer = 0;
    private const float LandTime = .12f;

    protected void Start()
    {
        GameController.Instance.AddCharacter(character, this);
        GameController.CharacterSwitchListener += OnCharacterSwitch;
        GameController.StageFinishedListener += DisableCharacter;
        _currentState = State.Idle;
        Velocity = rb2D.velocity;
    }
    private void OnDestroy()
    {
        GameController.CharacterSwitchListener -= OnCharacterSwitch;
        GameController.StageFinishedListener -= DisableCharacter;
    }
    private void DisableCharacter()
    {
        _disabled = true;
    }

    protected virtual void Update()
    {
        Velocity.y = rb2D.velocity.y;
        UpdateAnimation();
        if (!Current || _disabled) return;
        Velocity.x = Input.GetAxis("Horizontal") * walkSpeed;
        if (Velocity.x != 0) facingRight = Velocity.x > 0;
        transform.localScale = new Vector3(Math.Abs(transform.localScale.x) * (facingRight ? 1: -1), transform.localScale.y, transform.localScale.z);
        if (Grounded && Input.GetButtonDown("Jump")) Jump();
        if (_landTimer > 0) _landTimer -= Time.deltaTime;
        
    }

    private void UpdateAnimation()
    {
        if (Grounded)
        {
            if (Input.GetAxis("Vertical") < 0)
            {
                if (Velocity.x == 0)
                {
                    _landTimer = LandTime - 0.1f;
                    CurrentState = State.Landing;
                }
                else
                {
                    _landTimer = 0;
                    
                }
            }else if (CurrentState == State.Landing && _landTimer <= 0)
            {
                CurrentState = State.Idle;
            }
         
            if (CurrentState == State.Falling) CurrentState = State.Landing;
            else if(_landTimer <= 0) CurrentState = Velocity.x != 0 ? State.Walking : State.Idle;

        }
        else if (!Grounded || Velocity.y != 0) ;
        {
            if (Velocity.y > 2f) CurrentState = State.Jumping;
            else if (Velocity.y < -2f) CurrentState = State.Falling;
            else if(CurrentState == State.Jumping) CurrentState = State.Peak;
        }
        
        if (!stateChanged) return;
        stateChanged = false;
        switch (CurrentState)
        {
            case State.Idle: animator.Play("char_idle");
                break;
            case State.Walking: animator.Play("char_run");
                break;
            case State.Jumping: animator.Play("char_jump");
                break;
            case State.Peak: animator.Play("char_peak");
                break;
            case State.Falling: animator.Play("char_fall");
                break;
            case State.Landing: animator.Play("char_land");
                break;
        }
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(Velocity.x, rb2D.velocity.y);
    }

    protected void Jump()
    {
        CurrentState = State.Jumping;
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
        PlaySound(jump);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End")) GameController.UpdateFinishedChars(character,true);
        if(other.gameObject.CompareTag("Kill Plane")) GameController.Instance.ResetLevel();
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("End")) GameController.UpdateFinishedChars(character,false);
    }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")) && other.IsTouching(groundCheck))
        {
            if(!Grounded) PlaySound(land);
            Grounded = true;
        }

        if (other.gameObject.CompareTag("Airstream"))
        {
            
            //rb2D.velocity = new Vector2(rb2D.velocity.x, Mathf.Clamp(rb2D.velocity.y, -20, 20));
            if(rb2D.velocity.y < 13)rb2D.AddForce(new Vector2(0,22));
            Grounded = false;
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
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
