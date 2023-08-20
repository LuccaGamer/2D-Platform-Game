using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerController : MonoBehaviour
{   
    internal static PlayerController player;
    [SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float runSpeed = 15f;

    TouchingDirections touchingDirections;
    private Damageable damage;
    public Animator deathMenuAnim;


    public float CurrentMoveSpeed
    {
        get
        {
            if (damage.IsAlive)
            {
                if(IsMoving && !touchingDirections.IsOnWall)
                {
                    if(IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                        return walkSpeed;
                }
                else
                    return 0;
            }
            else 
                return 0;
        }
    }
    Rigidbody2D rb;
    Vector2 moveInput;
    Animator anim;
    public float jumpForce = 5f;
    private bool _isMoving = false;
    public bool IsMoving 
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            anim.SetBool(AnimationStrings.isMoving, value);
        }
    }

    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            anim.SetBool(AnimationStrings.isRunning, value);
        }
    }

    private bool _isFacingRight = true;
    

    public bool isFacingRight{
        get
        {
            return _isFacingRight;
        }
        set
        {
            if ( _isFacingRight != value)
                transform.localScale *= new Vector2 ( -1, 1);

            _isFacingRight = value;
        }
        
    }

   
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damage = GetComponent<Damageable>();
    }

    private void FixedUpdate() {
        if(!damage.lockVelocity)
            rb.velocity = new Vector2 (moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        anim.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (!damage.IsAlive)
        {
            deathMenuAnim.SetBool(AnimationStrings.isShown, true);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if ( moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if ( moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded)
        {
            anim.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anim.SetTrigger(AnimationStrings.attack);
        }
    }
    
    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2 (knockback.x, rb.velocity.y + knockback.y);

    }
    
    
}
