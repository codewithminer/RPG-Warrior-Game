using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entityVfx;
    private Entity_Stats stats;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void PreformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable == null)
                return;

            float elementalDamage = stats.GetElementalDamage(out ElementType element);
            float damage = stats.GetPhysicalDamage(out bool isCrit);
            bool targetGotHit = damageable.TakeDamage(damage, elementalDamage, element, transform);
            if (targetGotHit)
                entityVfx.CreateOnHitVFX(target.transform, isCrit);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
