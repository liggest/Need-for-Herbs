using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot2 : MonoBehaviour
{
    //public int slotID;
    public PropItem slotItem;
    public Image slotImage;
    //public Image material1;
    //public Image material2;
    //public int material1num;
    //public int material2num;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryManager.PanelClear();
        InventoryManager.UpdatePropItem(slotItem,slotItem.material1.itemImage,slotItem.material2.itemImage, slotItem.material1num, slotItem.material2num,
            slotItem.itemInfo);
    }

    public void SetupSlot(PropItem item)
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