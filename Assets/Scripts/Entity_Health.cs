using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity entity;
    private Entity_VFX entityVfx;

    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    [Header("On Damage Knockback")]
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private Vector2 onDamageKnockback = new Vector2(1.5f, 2.5f);
    [Space]
    [Range(0, 1)]
    [SerializeField] private float heavyDamageThreshhold = .3f;
    [SerializeField] private float heavyKnockbackDuration = .5f;
    [SerializeField] private Vector2 onHeavyDamageKnockback = new Vector2(7, 7);


    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVfx = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return;

        float duration = CalculateDuration(damage);
        Vector2 knockback = CalculateKnockbackDirection(damage, damageDealer);
        
        entityVfx?.PlayOnDamageVfx();
        entity?.ReciveKnockback(knockback, duration);
        ReduceHp(damage);
    }

    protected void ReduceHp(float damage)
    {
        maxHp -= damage;
        if (maxHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
    }

    private Vector2 CalculateKnockbackDirection(float damage, Transform damageDealer)
    {
        int direction = transform.position.x > damageDealer.position.x ? 1 : -1;
        Vector2 knockback = IsHeavyDamage(damage) ? onHeavyDamageKnockback : onDamageKnockback;
        knockback.x *= direction;
        return knockback;
    }

    private float CalculateDuration(float damage)
    {
        return IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }

    private bool IsHeavyDamage(float damage) => damage / maxHp > heavyDamageThreshhold;
}
