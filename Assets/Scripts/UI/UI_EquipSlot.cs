using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipSlot : UI_ItemSlot
{
   public ItemType itemType;

    private void OnValidate()
    {
        gameObject.name = "UI_EquipmentSlot - " + itemType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;
        
        inventory.UnequipItem(itemInSlot);
    }
}
