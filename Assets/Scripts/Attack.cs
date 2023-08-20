using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damageAmount = 20f;
    public Vector2 knockback = Vector2.zero;

    public float attackCooldown = 0.5f;
    private float lastAttack = 0;


    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damage = other.GetComponent<Damageable>();
        
        if (damage != null)
        {
            Vector2 deliveryKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            
            if(Time.time - lastAttack > attackCooldown)
            {
            bool gotHit = damage.Hit(damageAmount, deliveryKnockback);
            lastAttack = Time.time;
            }
        }
    }

}
