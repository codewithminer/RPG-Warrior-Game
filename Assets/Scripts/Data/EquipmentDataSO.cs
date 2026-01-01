using System;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Equipment item", fileName ="Equipment data - ")]
public class EquipmentDataSO : ItemDataSO
{
    [Header("Item modifires")]
    public ItemModifire[] modifires;
}

[Serializable]
public class ItemModifire
{
    public StatType statType;
    public float value;
}
