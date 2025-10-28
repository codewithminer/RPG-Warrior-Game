using System;
using UnityEngine;

[Serializable]
public class Stat_DefenseGroup
{
    // physical defense
    public Stat armor;
    public Stat evasion;

    // elemental defense
    public Stat fireRes; // fireResist
    public Stat iceRes;
    public Stat lightingRes;
}
