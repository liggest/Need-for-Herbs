using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemOnWorld : MonoBehaviour
{
    public GameObject player;
    public Item thisItem;
    public Inventory playerInventory;
    public GameObject BagFullPanel;
    public Text BagFullPanelTips;
    public Animator animator;
    public bool iscanpick;

    [Header("加载条")]
    public int t;
    public float DownDistance;
    float progressvalue;
    public Slider slider;
    bool isLoad;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag( "Player"))
        {
            animator.SetTrigger("istrigger");
            iscanpick = true;
        }
       
        //StartCoroutine(trigger());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag( "Player"))
        {
            iscanpick = false;
            stopload();
        }      
    }

    public void pick()
    {
        AddNewItem();
        if (thisItem.Itemid == 10)
        {
            RandomManager.rm.RemoveSuperHerbs(gameObject);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!isLoad)
        {
            if (Input.GetKeyDown(KeyCode.Y) && iscanpick)
            {
                if (!isFull())
                {
                    load();
                }
                else
                {
                    if (playerInventory.itemList.Contains(thisItem))
                    {
                        load();
                    }
                    else
                    {
                        BagFullPanel.SetActive(true);
                        BagFullPanelTips.text = "背包已满";
                    }     
                }
            }
        }
        ////
        if (isLoad)
        {
            SetPos();
            progressvalue += Time.deltaTime;
            if (progressvalue > t)
            {
                progressvalue = t;
            }
            slider.value = progressvalue / t;
            if (slider.value == 1)
            {
                slider.gameObject.SetActive(false);
                isLoad = false;
                pick();
            }
        }
    }

    void Start()
    {
        isLoad = false;
        player = GameCtrl.gc.Player.gameObject;
        BagFullPanel = GameCtrl.gc.BagFullPanel;
        BagFullPanelTips = GameCtrl.gc.BagFullPanelTips;
        slider = GameCtrl.gc.slider;

    }

    public void load()
    {
        progressvalue = 0;
        isLoad = true;
        slider.gameObject.SetActive(true);
    }


    public void stopload()
    {
        if (isLoad)
        {
            isLoad = false;
            progressvalue = 0;
            slider.gameObject.SetActive(false);
        }    
    }


    void SetPos()
    {
        slider.transform.position = transform.position + -1 * transform.up * DownDistance;
    }

    public  bool isFull()
    {
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i]==null)
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
