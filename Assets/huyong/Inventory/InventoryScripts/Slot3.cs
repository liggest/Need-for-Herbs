using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot3 : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo1(slotItem.itemName,slotItem.Value,slotItem.itemInfo,slotItem.attackValue,slotItem.spellPower,slotItem.Armor,
            slotItem.spellResistance,slotItem.criticalChance,slotItem.criticalDamage,slotItem.Speed,slotItem.healthValue,slotItem.healthSteal,slotItem.effect,slotItem.itemImage);
        slotID = itemInSlot.GetComponentInParent<Slot3>().slotID;
        InventoryManager.getID(slotID,false);
    }

    public void SetupSlot(Item item) 
    {
        if (item == null) 
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotItem = item;
    }
}