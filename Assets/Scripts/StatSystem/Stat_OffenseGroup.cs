using System;
using UnityEngine;

[Serializable]
public class Stat_OffenseGroup
{
    // physical damage
    public Stat damage;
    public Stat critPower;
    public Stat critChance;

    // Elemental damage
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightingDamage;
}
