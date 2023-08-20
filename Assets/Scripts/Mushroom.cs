using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof (TouchingDirections))]
public class Mushroom : MonoBehaviour
{   
    public float walkAccelerations = 5f;
    public float walkSpeed = 5f;
    TouchingDirections touchingDirections;
    Animator anim;
    Rigidbody2D rb;
    public DetectionZone attackZone;
    Damageable dmg;

    public enum WalkableDirections { Right, Left}

    public WalkableDirections _walkableDirections;
    private Vector2 walkDirectionVector = Vector2.right;

    private bool _hasTarget;

    public bool HasTarget{
        get{ return _hasTarget; }
        set{
            _hasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public WalkableDirections walkableDirections{
        get {
            return _walkableDirections;
        }
        set{
            if (_walkableDirections != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirections.Left)
                {
                    walkDirectionVector = Vector2.left;
                } else if (value == WalkableDirections.Right)
                {
                    walkDirectionVector = Vector2.right;
                }

            }
            _walkableDirections = value;
        }
    }

    public bool CanMove{
        get{
            return anim.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        anim = GetComponent<Animator>();
        dmg = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate() {
        if(touchingDirections.IsOnWall && touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
        if (!dmg.lockVelocity)
        {
            if (CanMove)
             
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            else
                rb.velocity = Vector2.zero;
        }
    }

    private void FlipDirection()
    {
        if (walkableDirections == WalkableDirections.Left)
        {
            walkableDirections = WalkableDirections.Right;
        }
        else if (walkableDirections == WalkableDirections.Right)
        {
            walkableDirections = WalkableDirections.Left;
        }
        else
        Debug.Log("error direction");
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2 (knockback.x, rb.velocity.y + knockback.y);
    }

    public void GroundDetection()
    {
        if (touchingDirections.IsGrounded)
            FlipDirection();
    }
}
