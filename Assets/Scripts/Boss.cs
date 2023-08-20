using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rageMoveSpeed = 7f;
    public DetectionZone playerDetectionZone;
    public float attackAnimationDelayTime = 1f;
    Animator anim;
    Rigidbody2D rb;
    Damageable damage;
    private Transform character;
    public float minDistanceToPlayer = 0.5f;
    public DetectionZone targetDetectionZone;

    public bool CanMove { get{
        return anim.GetBool(AnimationStrings.canMove);
    } }

    private bool _EnterBossFight = false;
    public bool OnEnterBossFight { get{
        return _EnterBossFight;

    } private set{
        anim.SetBool(AnimationStrings.playerInRange, value);
        _EnterBossFight = value;
    } }

    private void Awake() {
        rb = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        damage = GetComponent<Damageable> ();
        character = GameObject.FindGameObjectWithTag ("Player") .transform;
    }


    private void FixedUpdate() {
        if (damage.IsAlive)
        {
            if (OnEnterBossFight)
            {
                if (CanMove)
                {   
                    if (!damage.lockVelocity)
                        BossMove();
                    else
                        rb.velocity = Vector3.zero;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        
    }

    public void Raging()
    {
        if (damage.Health < damage.MaxHealth/2)
                {
                    if(IsRage == false)
                        IsRage = true;
                }
    }

    public bool hasTarget{
        get {
            return anim.GetBool(AnimationStrings.hasTarget);
        }
        private set{
            anim.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    
    private bool _isRage = false;
    public bool IsRage { get{
        return _isRage;
    } private set{
        if (_isRage != value)
        {
            _isRage = value;
            anim.SetBool(AnimationStrings.isRage, value);
        }
    } }

    private Vector2 moveDirection;
    private void BossMove()
    {
        Vector2 moveDirection = (character.position - transform.position).normalized;
        if (!IsRage)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
            FacingPlayer();
        }
        else if (IsRage)
        {
            rb.velocity = new Vector2(moveDirection.x * rageMoveSpeed, rb.velocity.y);
            FacingPlayer();
        }
    }

    private void FacingPlayer()
    {   
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0 )
        {
            if (rb.velocity.x < 0)
            {
                // character is on the right
                transform.localScale = new Vector3( -1 *locScale.x, locScale.y, 0);
            } 
        }
        else if (transform.localScale.x < 0)
        {
            //player is on the left
            if (rb.velocity.x > 0)
            {
            transform.localScale = new Vector3(-1 * locScale.x, locScale.y,0);

            }
        }
    }

    
    public void PlayerCheck()
    {
        if  (targetDetectionZone.detectedColliders.Count > 0)
        {
            hasTarget = true;
        }
        
    }


    private float lastAttack;

    private void Update() {
        if (playerDetectionZone.detectedColliders.Count > 0)
            OnEnterBossFight = true;
       
        int atk = UnityEngine.Random.Range(0,2);
        PlayerCheck();
        Raging();
        if (hasTarget)
        {
            if (Time.time - lastAttack > attackAnimationDelayTime)
            {
                if (IsRage)
                {
                    if (atk == 0)
                    {
                        anim.SetTrigger(AnimationStrings.attack_1);
                        lastAttack = Time.time;
                    } else if (atk == 1)
                    {
                        anim.SetTrigger(AnimationStrings.attack_3);
                        lastAttack = Time.time;
                    }
                }
                else
                {
                    anim.SetTrigger(AnimationStrings.attack_2);
                    lastAttack = Time.time;
                }
            }
        }

        if(!damage.IsAlive)
        {
            Invoke("Scn", 2);
        }
        
    }

    public void Scn()
    {
            SceneManager.LoadScene("Victory");

    }

}
