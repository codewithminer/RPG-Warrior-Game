using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField] private float baseValue;
    [SerializeField] private List<StatModifire> modifires = new List<StatModifire>();

    private bool needToCalculate = true;
    private float finalValue;

    public float GetValue()
    {
        if (needToCalculate)
        {
            finalValue = GetFinalValue();
            needToCalculate = false;
        }

        return finalValue;
    }


    public void AddModifire(float value, string source)
    {
        StatModifire modToAdd = new StatModifire(value, source);
        modifires.Add(modToAdd);
        needToCalculate = true;
    }

    public void RemoveModifire(string source)
    {
        modifires.RemoveAll(modifier => modifier.source == source);
        needToCalculate = true;
    }

    private float GetFinalValue()
    {
        float finalValue = baseValue;

        foreach (var modifier in modifires)
        {
            finalValue += modifier.value;
        }

        return finalValue;
    }
}

[Serializable]
public class StatModifire
{
    public float value;
    public string source;

    public StatModifire(float value, string source)
    {
        this.value = value;
        this.source = source;
    }
}
