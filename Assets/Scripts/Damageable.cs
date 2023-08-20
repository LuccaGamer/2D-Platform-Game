using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{

    public UnityEvent<float, Vector2> damageableHit; 
    Animator anim;
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _health = 100;
    private bool _isAlive = true;

    public float MaxHealth{
        get { return _maxHealth;
        }
        set {
            _maxHealth = value;
        }
    }


    public float Health{
        get { return _health; }
        set {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;   
            }
        }
    }

    public bool IsAlive { get{
        return _isAlive;
    } private set{
        _isAlive = value;
        anim.SetBool(AnimationStrings.isAlive, value);
        Debug.Log("IsAlive set" + value);
    } }

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public float invincibleCooldown = 0.5f;
    private float lastInvincible;

    public bool lockVelocity { get{
        return anim.GetBool(AnimationStrings.lockVelocity);
    }
    set{
        anim.SetBool(AnimationStrings.lockVelocity, value);
    } }


    public bool isInvincible = false;

    // Start is called before the first frame update
    public bool Hit(float damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            lastInvincible = Time.time;
            lockVelocity = true;
            anim.SetTrigger(AnimationStrings.hitTrigger);
            damageableHit.Invoke(damage, knockback);
            CharacterEvents.CharacterDamaged(gameObject, damage);
            
            return true;

        }
        else
            return false;
    }

    public bool Heal(float healAmount)
    {
        if(IsAlive && Health < MaxHealth)
        {   
            float healLimit = Mathf.Max(MaxHealth - Health, 0);
            float totalHealAmount = Mathf.Min(healLimit, healAmount);
            CharacterEvents.CharacterHealed(gameObject, totalHealAmount);
            Health += totalHealAmount;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Update() {
        if (Time.time - lastInvincible > invincibleCooldown)
        {
            isInvincible = false;
        }
    }
}
