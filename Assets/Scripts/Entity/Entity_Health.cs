using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Slider healthBar;
    private Entity entity;
    private Entity_Stats stats;
    private Entity_VFX entityVfx;

    [SerializeField] protected float currentHp;
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
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();
        currentHp = stats.GetMaxHealth();
        UpdateHealthBar();
    }

    public virtual bool TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return false;

        if (AttackEvade())
        {
            Debug.Log($"{gameObject.name} evaded the attack!");
            return false;
        }

        float duration = CalculateDuration(damage);
        Vector2 knockback = CalculateKnockbackDirection(damage, damageDealer);

        entityVfx?.PlayOnDamageVfx();
        entity?.ReciveKnockback(knockback, duration);
        ReduceHp(damage);

        return true;
    }

    private bool AttackEvade() => Random.Range(0, 100) < stats.GetEvasion();

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        UpdateHealthBar();

        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealthBar()
    {
        if (healthBar == null)
            return;
        healthBar.value = currentHp / stats.GetMaxHealth();
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

    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHealth() > heavyDamageThreshhold;
}
