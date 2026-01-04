using System;
using UnityEngine;

[Serializable]
public class Inventory_Item
{
    private string itemId;

    public ItemDataSO itemData;
    public int stackSize = 1;

    public ItemModifire[] modifires {get; private set;}
    public ItemEffectDataSO itemEffect;

    public Inventory_Item(ItemDataSO itemData)
    {
        this.itemData = itemData;
        itemEffect = itemData.itemEffect;
        modifires = EquipmentData()?.modifires;

        itemId = itemData.itemName + " - " + Guid.NewGuid();
    }

    public void AddModifires(Entity_Stats playerStats)
    {
        foreach (var mod in modifires)
        {
            Stat statModify = playerStats.GetStatByType(mod.statType);
            statModify.AddModifire(mod.value, itemId);
        }
    }

    public void RemoveModifires(Entity_Stats playerStats)
    {
        foreach (var mod in modifires)
        {
            Stat statModify = playerStats.GetStatByType(mod.statType);
            statModify.RemoveModifire(itemId);
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
