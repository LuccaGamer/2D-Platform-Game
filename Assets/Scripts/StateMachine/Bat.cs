using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public DetectionZone attackDetectionZone;
    public List<Transform> waypoints;
    public float flyingSpeed = 5f;
    public float waypointDistance = 0.1f;

    Damageable damageable;

    private int waypointNum = 0;
    private Transform nextWaypoint;
    Animator anim;
    Rigidbody2D rb;
    


    private void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    
    private bool _hasTarget;

    public bool HasTarget{
        get{ return _hasTarget; }
        set{
            _hasTarget = value;
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove { get{
        return anim.GetBool(AnimationStrings.canMove);
    } }

    private void Start() {
        nextWaypoint = waypoints[waypointNum];
    }
    private void Update() {
        HasTarget = attackDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate() {
        if(damageable.IsAlive)
        {
            if(CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = UnityEngine.Vector2.zero; 
            }
        }
        else{
            rb.gravityScale = 2f;
        }
    }

    private void Flight()
    {
        UnityEngine.Vector2 flyingDirection = (nextWaypoint.transform.position - transform.position).normalized;

        float distance = UnityEngine.Vector2.Distance(nextWaypoint.transform.position, transform.position);

        rb.velocity = flyingDirection * flyingSpeed;
        FlipDirection();
        if (distance <= waypointDistance)
        {
            waypointNum ++;

            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void FlipDirection()
    {
        UnityEngine.Vector3 scaling = transform.localScale;
        if (transform.localScale.x > 0) 
        {
            //facing right
            if (rb.velocity.x < 0)
            {
                transform.localScale = new UnityEngine.Vector3(-1 * scaling.x, scaling.y, scaling.z);
            }
        }
        else if (transform.localScale.x < 0)
        {
            //facing left
            if (rb.velocity.x > 0)
            {
                transform.localScale = new UnityEngine.Vector3 (-1 * scaling.x, scaling.y, scaling.z);
            }
        }
    }
}
