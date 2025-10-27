using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    public Stat maxHp;
    public Stat vitality; // each point of vitality adds +5 HP

    public float GetMaxHealth()
    {
        float baseHp = maxHp.GetValue();
        float bounsHp = vitality.GetValue() * 5;

        return baseHp + bounsHp;
    }
}
