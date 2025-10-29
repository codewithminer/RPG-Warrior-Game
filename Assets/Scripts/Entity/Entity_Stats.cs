using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defence;

    public float GetMaxHealth()
    {
        float baseHp = maxHp.GetValue();
        float bounsHp = major.vitality.GetValue() * 5;

        return baseHp + bounsHp;
    }

    public float GetEvasion()
    {
        float baseEvasion = defence.evasion.GetValue();
        float bounsEvasion = major.agility.GetValue() * .5f; // each agility point gives you 0.5% of evasoin
        
        float totalEvasion = baseEvasion + bounsEvasion;
        float evasionCap = 85f;

        float finalEvasion = Mathf.Clamp(totalEvasion, 0, evasionCap); // be sure evasion does not exceed the cap
        return finalEvasion;
    }
}
