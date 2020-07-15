using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    //public Sprite image;
    public Inventory myBag;
    public Inventory myBag2;
    //public Inventory myBag3;
    public GameObject slotGrid1;
    public GameObject slotGrid2;
    public GameObject slotGrid3;

    GridLayoutGroup slotGL1;
    GridLayoutGroup slotGL2;
    GridLayoutGroup slotGL3;

    //public Text EnterBtn;

    //public Text itemName2;
    //public Text itemValue2;
    //public Image slotImage2;
    //public int effect2;
    //  public Slot slotPrefab;
    public Text itemInformation;

    public Image material1img;
    public Image material2img;
    public Sprite originmaterial1img;
    public Sprite originmaterial2img;

    public Text material1num;
    public Text material2num;

    public GameObject emptySlot;
    public GameObject emptySlot2;
    public GameObject emptySlot3;

    public List<GameObject> slots;
    public List<GameObject> slots2;
    public List<GameObject> slots3;


    //public Item thisItem;
    //public Item lastItem;
    //public Item changeBackItem;
    public List<Item> PropItemList;
    public List<EquipItem> EquipItemList;
    public PropItem currentItem;
   

    public GameObject TipsPanel;
    public Text Tips;
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    private Item currentChooseItem;
    private EquipItem currentChooseEquip;


    public PlayerCtrl player;
    public int taskpoint;
    //public bool useornot;

    //public GameObject choosePanel;
    //public Image leftBtn;
    //public Image centerBtn;
    //public Image rightBtn;


    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
       
    }

    private void Start()
    {
        slotGL1 = slotGrid1.GetComponent<GridLayoutGroup>();
        slotGL2 = slotGrid2.GetComponent<GridLayoutGroup>();
        slotGL3 = slotGrid3.GetComponent<GridLayoutGroup>();
        adaptGridSize(slotGL1);
        adaptGridSize(slotGL2);
        adaptGridSize(slotGL3);
        updateBag();
    }

    void adaptGridSize(GridLayoutGroup GL)
    {
        //Debug.Log("www");
        Vector2 originSize = GL.cellSize;
        GL.cellSize *= new Vector2(Screen.width, Screen.height) / new Vector2(800, 450); //800,450 经验值
        GL.spacing = originSize - GL.cellSize + GL.spacing; //经验公式
    }

    public static void updateBag()
    {
        instance.itemInformation.text = "";
        instance.currentItem = null;
        instance.material1img.sprite = instance.originmaterial1img;
        instance.material2img.sprite = instance.originmaterial2img;
        instance.material1num.text = "";
        instance.material2num.text = "";
        instance.panel1.SetActive(false);
        instance.panel2.SetActive(false);
        instance.panel3.SetActive(false);
        RefreshItem1();
        RefreshItem2();
        RefreshItem3();

        /*
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        if (instance.slotGL1)
        {
            Vector2 originSize = instance.slotGL1.cellSize;
            instance.slotGL1.cellSize *= new Vector2(Screen.width, Screen.height) / new Vector2(800, 450);
            instance.slotGL1.spacing = new Vector2(originSize.x - instance.slotGL1.cellSize.x, 0) + instance.slotGL1.spacing;
            originSize = instance.slotGL2.cellSize;
            instance.slotGL2.cellSize *= new Vector2(Screen.width, Screen.height) / new Vector2(800, 450);
            instance.slotGL2.spacing = new Vector2(originSize.x - instance.slotGL2.cellSize.x, 0) + instance.slotGL2.spacing;
            originSize = instance.slotGL3.cellSize;
            instance.slotGL3.cellSize *= new Vector2(Screen.width, Screen.height) / new Vector2(800, 450);
            instance.slotGL3.spacing = new Vector2(originSize.x - instance.slotGL3.cellSize.x, 0) + instance.slotGL3.spacing;
        }
        */

    }


    public static void UpdateItemInfo(string iteminfo)
    {
        instance.itemInformation.text = iteminfo;
    }


    public static void UpdatePropItem(PropItem PropTarget,Sprite material1image, Sprite material2image, int material1num, int material2num, string itemDescription)
    {
        instance.currentItem = PropTarget;
        instance.material1img.sprite = material1image;
        instance.material2img.sprite = material2image;
        instance.material1num.text = material1num.ToString();
        instance.material2num.text = material2num.ToString();
        instance.itemInformation.text = itemDescription;
    }

    public void Compose()
    {
        PanelClear();
        if(currentItem != null)
        {
            if (CheckMaterial())
            {
                int held1 = currentItem.material1.itemHeld - currentItem.material1num;
                int held2 = currentItem.material2.itemHeld - currentItem.material2num;
                bool isfull;
                if (isFull())
                {
                    if (held1 == 0 || held2 == 0)
                    {
                        isfull = false;
                    }
                    else
                        isfull = true;
                }
                else
                    isfull = false;

                if (!isfull)
                {
                    currentItem.material1.itemHeld = held1;
                    currentItem.material2.itemHeld = held2;

                    if (currentItem.material1.itemHeld == 0)
                    {
                        instance.myBag.itemList[instance.myBag.itemList.IndexOf(currentItem.material1)] = null;
                    }

                    if (currentItem.material2.itemHeld == 0)
                    {
                        instance.myBag.itemList[instance.myBag.itemList.IndexOf(currentItem.material2)] = null;
                    }

                    if (!instance.myBag.itemList.Contains(instance.PropItemList[currentItem.Itemid]))
                    {
                        for (int i = 0; i < instance.myBag.itemList.Count; i++)
                        {
                            if (instance.myBag.itemList[i] == null)
                            {
                                instance.myBag.itemList[i] = instance.PropItemList[currentItem.Itemid];
                                instance.PropItemList[currentItem.Itemid].itemHeld=1;
                                break;
                            }
                        }
                    }
                    else
                    {
                        instance.PropItemList[currentItem.Itemid].itemHeld += 1;
                    }
                    Changeherbstatus(currentItem.material1, currentItem.material1.itemHeld);
                    Changeherbstatus(currentItem.material2, currentItem.material2.itemHeld);
                    Changeherbstatus(instance.PropItemList[currentItem.Itemid], instance.PropItemList[currentItem.Itemid].itemHeld);
                    RefreshItem1();
                }
                else
                {
                    instance.TipsPanel.SetActive(true);
                    instance.Tips.text = "背包空间不足";
                }
            }
            else
            {
                instance.TipsPanel.SetActive(true);
                instance.Tips.text = "合成材料不足";
            }
        }

        else
        {
            instance.TipsPanel.SetActive(true);
            instance.Tips.text = "请选择合成物品";
        }
    }

    public void MouseLeftClick()
    {
        instance.panel2.SetActive(false);
        instance.panel1.SetActive(false);
        instance.panel3.SetActive(false);
    }
    public static void MouseRightClick1(Item slotItem)
    {
        float X = Input.mousePosition.x - Screen.width / 2f;
        float Y = Input.mousePosition.y - Screen.height / 2f;
        Vector2 tranPos = new Vector2(X, Y);
        if (slotItem.equip == false)
        {
            instance.panel2.SetActive(true);
            instance.panel1.SetActive(false);
            instance.panel3.SetActive(false);
            instance.panel2.GetComponent<RectTransform>().localPosition = tranPos;
        }

        if (slotItem.equip == true)
        {
            instance.panel2.SetActive(false);
            instance.panel1.SetActive(true);
            instance.panel3.SetActive(false);
            instance.panel1.GetComponent<RectTransform>().localPosition = tranPos;
        }

        instance.currentChooseItem = slotItem;
    }

    public static void MouseRightClick2(EquipItem equip)
    {
        float X = Input.mousePosition.x - Screen.width / 2f;
        float Y = Input.mousePosition.y - Screen.height / 2f;
        Vector2 tranPos = new Vector2(X, Y);
        instance.panel2.SetActive(false);
        instance.panel1.SetActive(false);
        instance.panel3.SetActive(true);
        instance.panel3.GetComponent<RectTransform>().localPosition = tranPos;
        instance.currentChooseEquip = equip;
    }

    public void Changeherbstatus(Item item,int num)
    {
        if (GameCtrl.gc.taskherb.Contains(item))
        {
            int index = GameCtrl.gc.taskherb.IndexOf(item);
            GameCtrl.gc.taskherbnum[index] = num;
            if (GameCtrl.gc.taskherbnum[index]>= 
                GameCtrl.gc.taskherbtargetnum[index])
            {
                GameCtrl.gc.taskherbstatus[index] = true;
                GameCtrl.gc.isfinished = isFinished();
            }
            else
            {
                GameCtrl.gc.taskherbstatus[index] = false;
            }
        }
    }

    bool isFinished()
    {
        foreach (var status in GameCtrl.gc.taskherbstatus)
        {
            if (!status)
            {
                return false;
            }
        }
        return true;
    }

    public void Discard1()
    {
        Item item = instance.currentChooseItem;
        item.itemHeld = 0;
        instance.myBag.itemList[instance.myBag.itemList.IndexOf(item)] = null;
        Changeherbstatus(item, 0);
        PanelClear();
        RefreshItem1();
    }

    public void Discard2()
    {
        EquipItem item = instance.currentChooseEquip;
        instance.myBag.itemList3[instance.myBag.itemList3.IndexOf(item)] = null;
        player.UpdateProp();
        PanelClear();
        RefreshItem3();     
    }
    public void Equip()
    {
        Item item = instance.currentChooseItem;
        EquipItem equip = EquipItemList[item.Itemid];
        if (instance.myBag.itemList3.Contains(equip))
        {
            instance.TipsPanel.SetActive(true);
            instance.Tips.text = "该物品只可装备一个";
            PanelClear();
            return;
        }
        else
        {
            for (int i = 0; i < instance.myBag.itemList3.Count; i++)
            {
                if (instance.myBag.itemList3[i] == null)
                {
                    instance.myBag.itemList3[i] = equip;
                    item.itemHeld -= 1;
                    if (item.itemHeld == 0)
                    {
                        instance.myBag.itemList[instance.myBag.itemList.IndexOf(item)] = null;
                    }
                    player.UpdateProp();
                    PanelClear();
                    RefreshItem1();
                    RefreshItem3();
                    return;
                }
            }
            PanelClear();
            instance.TipsPanel.SetActive(true);
            instance.Tips.text = "装备区已满";
        }  
    }

    public void Remove()
    {
        EquipItem equip = instance.currentChooseEquip;
        Item item = PropItemList[equip.Itemid];
        if (!instance.myBag.itemList.Contains(item))
        {
            for (int i = 0; i < instance.myBag.itemList.Count; i++)
            {
                if (instance.myBag.itemList[i] == null)
                {
                    instance.myBag.itemList[i] = item;
                    instance.myBag.itemList3[instance.myBag.itemList3.IndexOf(equip)] = null;
                    item.itemHeld = 1;
                    player.UpdateProp();
                    RefreshItem1();
                    RefreshItem3();
                    PanelClear();
                    return;
                }
            }
            instance.TipsPanel.SetActive(true);
            instance.Tips.text = "背包已满";
            PanelClear();
            return;
        }
        else
        {
            instance.myBag.itemList3[instance.myBag.itemList3.IndexOf(equip)] = null;
            item.itemHeld += 1;
            player.UpdateProp();
            PanelClear();
            RefreshItem1();
            RefreshItem3();
        }
       
    }

    public static void PanelClear()
    {
        instance.panel1.SetActive(false);
        instance.panel2.SetActive(false);
        instance.panel3.SetActive(false);
    }


    public void AllPanelClear()
    {
        instance.panel1.SetActive(false);
        instance.panel2.SetActive(false);
        instance.panel3.SetActive(false);
    }

    public bool CheckMaterial()
    {
        Item material1 = currentItem.material1;
        int material1num = currentItem.material1num;
        Item material2 = currentItem.material2;
        int material2num = currentItem.material2num;
        if (!instance.myBag.itemList.Contains(material1))
        {
            return false;
        }
        if (!instance.myBag.itemList.Contains(material2))
        { 
            return false;
        }
        if (material1.itemHeld < material1num)
        {
            return false;
        }
        if (material2.itemHeld < material2num)
        {
            return false;
        }
        return true;

    }

    public static bool isFull()
    {
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            if (instance.myBag.itemList[i] == null)
            {
                return false;
            }
        }
        return true;
    }


    public static void RefreshItem1() 
    {
        for (int i = 0; i < instance.slotGrid1.transform.childCount; i++)
        {
            if (instance.slotGrid1.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid1.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid1.transform);
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
            instance.slots[i].GetComponent<Slot>().slotID = i;
        } 
    }

    public static void RefreshItem2()
    {
        for (int i = 0; i < instance.slotGrid2.transform.childCount; i++)
        {
            if (instance.slotGrid2.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid2.transform.GetChild(i).gameObject);
            instance.slots2.Clear();
        }

        for (int i = 0; i < instance.myBag.itemList2.Count; i++)
        {
            instance.slots2.Add(Instantiate(instance.emptySlot2));
            instance.slots2[i].transform.SetParent(instance.slotGrid2.transform);
            instance.slots2[i].GetComponent<Slot2>().SetupSlot(instance.myBag.itemList2[i]);
        }
    }

    public static void RefreshItem3()
    {
        for (int i = 0; i < instance.slotGrid3.transform.childCount; i++)
        {
            if (instance.slotGrid3.transform.childCount == 0)
            {
                break;
            }
            Destroy(instance.slotGrid3.transform.GetChild(i).gameObject);
            instance.slots3.Clear();
        }

        for (int i = 0; i < instance.myBag.itemList3.Count; i++)
        {
            // CreateNewItem(instance.myBag.itemList[i]); 
            instance.slots3.Add(Instantiate(instance.emptySlot3));
            instance.slots3[i].transform.SetParent(instance.slotGrid3.transform);
            instance.slots3[i].GetComponent<Slot3>().SetupSlot(instance.myBag.itemList3[i]);
        }
    }
}
