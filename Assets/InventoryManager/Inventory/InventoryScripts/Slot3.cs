using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot3 : MonoBehaviour
{
    //public int slotID;
    public EquipItem slotItem;
    public Image slotImage;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryManager.PanelClear();
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
    }

    public void OnRightClicked()
    {
        InventoryManager.MouseRightClick2(slotItem);
    }


    public void SetupSlot(EquipItem item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotItem = item;
    }
}