using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    public ItemDataSO itemData;
    public int stackSize = 1;

    public ItemModifire[] modifires {get; private set;}

    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
        modifires = EquipmentData()?.modifires;
    }

    public void AddModifires(Entity_Stats playerStats)
    {
        foreach (var mod in modifires)
        {
            Stat statModify = playerStats.GetStatByType(mod.statType);
            statModify.AddModifire(mod.value, itemData.itemName);
        }
    }

    public void RemoveModifires(Entity_Stats playerStats)
    {
        foreach (var mod in modifires)
        {
            Stat statModify = playerStats.GetStatByType(mod.statType);
            statModify.RemoveModifire(itemData.itemName);
        }
    }

    private EquipmentDataSO EquipmentData()
    {
        if (itemData is EquipmentDataSO equipment)
            return equipment;

        return null;
    }

    public bool CanAddStack() => stackSize < itemData.maxStackSize;

    public void AddStack() => stackSize++;

    public void RemoveStack() => stackSize--;
}
