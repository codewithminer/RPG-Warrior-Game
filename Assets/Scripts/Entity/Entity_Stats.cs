using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defence;


    public float GetPhysicalDamage(out bool isCrit)
    {
        float baseDamage = offense.damage.GetValue();
        float bonusDamge = major.strength.GetValue();
        float totalDamage = baseDamage + bonusDamge;

        float baseCritChance = offense.critChance.GetValue();
        float bonusCritChance = major.agility.GetValue() * .3f;
        float critChance = baseCritChance + bonusCritChance;

        float baseCritPower = offense.critPower.GetValue();
        float bonusCritPower = major.strength.GetValue() * .5f;
        float critPower = (baseCritPower + bonusCritPower) / 100;

        isCrit = Random.Range(0, 100) < critChance;
        float finalDamage = isCrit ? totalDamage * critPower : totalDamage;

        return finalDamage;
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = maxHp.GetValue();
        float bounsMaxHealth = major.vitality.GetValue() * 5;
        float finalMaxHealth = baseMaxHealth + bounsMaxHealth;
        
        return finalMaxHealth;
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
