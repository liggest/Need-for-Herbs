using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialize : MonoBehaviour
{
    public Inventory mybag;
    public List<Item> Listitem;
    public PlayerCtrl player;
    // Start is called before the first frame update
    void Start()
    {
        string scenename = SceneManager.GetActiveScene().name;
        if (scenename == "Tutorial")
        {
            for (int i = 0; i < mybag.itemList.Count; i++)
            {
                mybag.itemList[i] = null;
            }

            for (int i = 0; i < mybag.itemList3.Count; i++)
            {
                mybag.itemList3[i] = null;
            }

            for (int i = 0; i < Listitem.Count; i++)
            {
                mybag.itemList[i] = Listitem[i];
                mybag.itemList[i].itemHeld = 20;
            }
        }

        if (scenename=="Main")
        {
            for (int i = 0; i < mybag.itemList.Count; i++)
            {
                mybag.itemList[i] = null;
            }

            for (int i = 0; i < mybag.itemList3.Count; i++)
            {
                mybag.itemList3[i] = null;
            }

            player.initialized();
        }
        GameCtrl.gc.isfinished = false;
        InventoryManager.updateBag();
        player.UpdateProp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
