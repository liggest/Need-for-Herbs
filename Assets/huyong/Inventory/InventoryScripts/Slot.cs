using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public Item3 thisItem;
    public Item lastItem;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo1(slotItem.itemName,slotItem.Value,slotItem.itemInfo,slotItem.attackValue,slotItem.spellPower,slotItem.Armor,
            slotItem.spellResistance,slotItem.criticalChance,slotItem.criticalDamage,slotItem.Speed,slotItem.healthValue,slotItem.healthSteal,slotItem.effect,slotItem.itemImage);
        lastItem = slotItem;
        slotID = itemInSlot.GetComponentInParent<Slot>().slotID;
        InventoryManager.getID(slotID,true);
        Debug.Log(slotID);
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