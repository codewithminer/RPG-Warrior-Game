using System.Threading.Tasks;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{

    [Header("Target detection")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;


    public void PreformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            Debug.Log($"{gameObject.name} attacked {target.name}");   
        }
    }

    private Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }

}
