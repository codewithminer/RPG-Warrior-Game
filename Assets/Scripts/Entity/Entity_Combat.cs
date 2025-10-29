using System.Threading.Tasks;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private Entity_VFX entityVfx;
    public float damage = 10;

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
    }

    public void PreformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            IDamageable damageable = target.GetComponent<IDamageable>();
            if (damageable == null)
                return;
            bool targetGotHit = damageable.TakeDamage(damage, transform);
            if (targetGotHit)
                entityVfx.CreateOnHitVFX(target.transform);
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
