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
}
