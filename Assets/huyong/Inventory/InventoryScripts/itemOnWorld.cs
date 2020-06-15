using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemOnWorld : MonoBehaviour
{
    public GameObject player;
    public Item thisItem;
    public Inventory playerInventory;
    public Inventory Bag;
    public GameObject BagFullPanel;
    // Start is called before the first frame update

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
        
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        if (Input.GetKeyDown(KeyCode.Y) && !isFull())
    //        {
    //            AddNewItem();
    //            Destroy(gameObject);
    //        }
    //        else
    //        {
    //            BagFullPanel.SetActive(true);
    //        }    
    //    }
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y)&& Vector3.Distance(GetComponent<Transform>().position, player.transform.position) <= 1f)
        {
            if (!isFull())
            {
                AddNewItem();
                Destroy(gameObject);
            }
            else
            {
                BagFullPanel.SetActive(true);
            }
        }      
    }

    public  bool isFull()
    {
        for (int i = 0; i < Bag.itemList.Count; i++)
        {
            if (Bag.itemList[i]==null)
            {
                return false;
            }
        }
        return true;
    }

    public void AddNewItem()
    {
        if (!playerInventory.itemList.Contains(thisItem))
        {
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    thisItem.itemHeld = 1;
                    break;
                }
            }
        }
        else
        {
            thisItem.itemHeld += 1;
        }
        InventoryManager.RefreshItem1();
    }

}
