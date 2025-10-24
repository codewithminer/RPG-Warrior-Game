using System;
using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Entity entity;
    private Entity_VFX entityVfx;

    [SerializeField] protected float currentHp;
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
        entity = GetComponent<Entity>(); // returns the already existing Enemy, Player, or any subclass of Entity component attached to that GameObject — not a new one.
        entityVfx = GetComponent<Entity_VFX>();
        currentHp = maxHp;
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
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
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
