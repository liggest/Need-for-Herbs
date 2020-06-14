using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot2 : MonoBehaviour
{
    public int slotID;
    public Item2 slotItem;
    public Image slotImage;
    public Text slotNum;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {

        InventoryManager.UpdateItemInfo2(slotItem.itemName,slotItem.Value,slotItem.itemInfo,slotItem.effect,slotItem.itemImage);
    }

    public void SetupSlot(Item2 item) 
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