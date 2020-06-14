using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld2 : MonoBehaviour
{
    public Item2 thisItem;
    public Inventory playerInventory;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList2.Contains(thisItem))
        {
            //playerInventory.itemList.Add(thisItem);
            for (int i = 0; i < playerInventory.itemList2.Count; i++)
            {
                if (playerInventory.itemList2[i]==null)
                {
                    playerInventory.itemList2[i] = thisItem;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem2();
    }

}
