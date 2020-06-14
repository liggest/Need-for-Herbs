using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    static InventoryManager instance;
    public Sprite image;
    public Inventory myBag;
    public Inventory myBag2;
    public Inventory myBag3;
    public GameObject slotGrid1;
    public GameObject slotGrid2;
    public GameObject slotGrid3;

    public Text EnterBtn;

    public Text itemName2;
    public Text itemValue2;
    public Image slotImage2;
    public int effect2;
    //  public Slot slotPrefab;
    public Text itemInformation;
    public Text itemInformation2;

    public Text itemName;
    public Text itemValue;
    public Text attackValue;
    public Text spellPower;
    public Text Armor;
    public Text spellResistance;
    public Text criticalChance;
    public Text criticalDamage;
    public Text Speed;
    public Text healthValue;
    public Text healthSteal;
    public Image slotImage;
    public int effect;

    public GameObject emptySlot;
    public GameObject emptySlot2;
    public GameObject emptySlot3;

    public List<GameObject> slots = new List<GameObject>();
    public List<GameObject> slots2 = new List<GameObject>();
    public List<GameObject> slots3 = new List<GameObject>();


    public Item thisItem;
    public Item lastItem;
    public Item changeBackItem;
    public int slotID;
    public bool useornot;

    public GameObject choosePanel;
    public Image leftBtn;
    public Image centerBtn;
    public Image rightBtn;
    public void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void OnEnable()
    {
        instance.itemName2.text="普通的药水";
        instance.itemValue2.text="0";
        instance.slotImage2.sprite=image;
        instance.effect2=0;
        instance.itemInformation2.text = "这是一瓶普通的药水";

        instance.slotImage.sprite = image;
        instance.itemName.text = "普通的装备";
        instance.itemValue.text = "0";
        instance.attackValue.text = "攻击力：+0" ;
        instance.spellPower.text = "法术强度：+0";
        instance.Armor.text = "护甲：+0";
        instance.spellResistance.text = "魔抗：+0" ;
        instance.criticalChance.text = "暴击率：+0" ;
        instance.criticalDamage.text = "暴击伤害：+0";
        instance.Speed.text = "移动速度：+0" ;
        instance.healthValue.text = "生命值：+0" ;
        instance.healthSteal.text = "生命偷取：+0" ;
        instance.itemInformation.text = "这是一件普通的装备";
        RefreshItem1();
        RefreshItem2();
        RefreshItem3();
        choosePanel.SetActive(false);
    }
    
   

    public static void UpdateItemInfo1(string itemName,int itemValue, string itemDescription, float attackValue,
        float spellPower ,float Armor, float spellResistance,float criticalChance, float criticalDamage,float Speed,
        float healthValue, float healthSteal, int effect,Sprite image)
    {
        instance.slotImage.sprite = image;
        instance.itemName.text = itemName;
        instance.itemValue.text = itemValue.ToString();
        instance.attackValue.text = "攻击力：+" + attackValue.ToString();
        instance.spellPower.text = "法术强度：+" + spellPower.ToString();
        instance.Armor.text = "护甲：+" + Armor.ToString();
        instance.spellResistance.text = "魔抗：+" + spellResistance.ToString();
        instance.criticalChance.text = "暴击率：+" + criticalChance.ToString();
        instance.criticalDamage.text = "暴击伤害：+" + criticalDamage.ToString();
        instance.Speed.text = "移动速度：+" + Speed.ToString();
        instance.healthValue.text = "生命值：+" + healthValue.ToString();
        instance.healthSteal.text = "生命偷取：+" + healthSteal.ToString();
        instance.effect= effect;
        instance.itemInformation.text = itemDescription;
    }

    public static void UpdateItemInfo2(string itemName, int itemValue, string itemDescription, int effect, Sprite image)
    {
        instance.slotImage2.sprite = image;
        instance.itemName2.text = itemName;
        instance.itemValue2.text = itemValue.ToString();
        instance.effect2 = effect;
        instance.itemInformation2.text = itemDescription;
    }


    public static void getID(int ID,bool b)
    {
        instance.slotID = ID;
        instance.useornot = b;
        if (b)
        {
            instance.EnterBtn.text = "装  备";
        }
        else
        {
            instance.EnterBtn.text = "卸  下";
        }
        Debug.Log(instance.slotID);
    }

    public static void getItem()
    {
        instance.lastItem =instance.myBag.itemList[instance.slotID];
        instance.thisItem = ScriptableObject.CreateInstance<Item>();
        instance.thisItem.itemName = instance.lastItem.itemName;
        instance.thisItem.itemImage = instance.lastItem.itemImage;
        instance.thisItem.itemHeld = instance.lastItem.itemHeld;
        instance.thisItem.Value = instance.lastItem.Value;
        instance.thisItem.attackValue = instance.lastItem.attackValue;
        instance.thisItem.spellPower = instance.lastItem.spellPower;
        instance.thisItem.Armor = instance.lastItem.Armor;
        instance.thisItem.spellResistance = instance.lastItem.spellResistance;
        instance.thisItem.criticalChance = instance.lastItem.criticalChance;
        instance.thisItem.criticalDamage = instance.lastItem.criticalDamage;
        instance.thisItem.Speed = instance.lastItem.Speed;
        instance.thisItem.healthValue = instance.lastItem.healthValue;
        instance.thisItem.healthSteal = instance.lastItem.healthSteal;
        instance.thisItem.effect = instance.lastItem.effect;
        instance.thisItem.itemInfo = instance.lastItem.itemInfo;
        UnityEditor.AssetDatabase.CreateAsset(instance.thisItem, "Assets/Inventory/Items/item3/New Item3.asset");
    }
    public static void getItem2()
    {
        instance.changeBackItem = ScriptableObject.CreateInstance<Item>();
        instance.changeBackItem.itemName = instance.thisItem.itemName;
        instance.changeBackItem.itemImage = instance.thisItem.itemImage;
        instance.changeBackItem.itemHeld = instance.thisItem.itemHeld;
        instance.changeBackItem.Value = instance.thisItem.Value;
        instance.changeBackItem.attackValue = instance.thisItem.attackValue;
        instance.changeBackItem.spellPower = instance.thisItem.spellPower;
        instance.changeBackItem.Armor = instance.thisItem.Armor;
        instance.changeBackItem.spellResistance = instance.thisItem.spellResistance;
        instance.changeBackItem.criticalChance = instance.thisItem.criticalChance;
        instance.changeBackItem.criticalDamage = instance.thisItem.criticalDamage;
        instance.changeBackItem.Speed = instance.thisItem.Speed;
        instance.changeBackItem.healthValue = instance.thisItem.healthValue;
        instance.changeBackItem.healthSteal = instance.thisItem.healthSteal;
        instance.changeBackItem.effect = instance.thisItem.effect;
        instance.changeBackItem.itemInfo = instance.thisItem.itemInfo;
        UnityEditor.AssetDatabase.CreateAsset(instance.changeBackItem, "Assets/Inventory/Items/item1/New Item.asset");
    }

    public void putEquip()
    {
        if (useornot) { 
            //getItem();
            for (int i = 0; i < instance.myBag3.itemList3.Count; i++)
            {
                if (instance.myBag3.itemList3[i] == null)
                {
                    //instance.myBag3.itemList3[i] = instance.thisItem;
                    instance.myBag3.itemList3[i] = instance.myBag.itemList[instance.slotID];
                    instance.myBag.itemList[instance.slotID] = null;
                    //instance.lastItem.itemHeld += -1;
                    //if (instance.lastItem.itemHeld == 0)
                    //{
                    //    instance.myBag.itemList[instance.slotID] = null;
                    //}
                    InventoryManager.RefreshItem1();
                    InventoryManager.RefreshItem3();
                    return;
                }
            }
            ChooseEquipSet();
        }
        else
        {
            if (!isFull())
            {
                instance.thisItem = instance.myBag3.itemList3[instance.slotID];
                instance.myBag3.itemList3[instance.slotID] = null;
                getItem2();
                for (int i = 0; i < instance.myBag.itemList.Count; i++)
                {
                    if (instance.myBag.itemList[i] == null)
                    {
                        instance.myBag.itemList[i] = changeBackItem;
                        break;
                    }
                }
                InventoryManager.RefreshItem1();
                InventoryManager.RefreshItem3();
                return;
            }
        }
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

    public void ChooseEquipSet()
    {
        choosePanel.SetActive(true);
        leftBtn.sprite = instance.myBag3.itemList3[0].itemImage;
        centerBtn.sprite = instance.myBag3.itemList3[1].itemImage;
        rightBtn.sprite = instance.myBag3.itemList3[2].itemImage;

    }

    public void ChooseEnter(int i)
    {
        switch (i){
            case 0:
                var toolitem = myBag3.itemList3[0];
                instance.myBag3.itemList3[0] = instance.myBag.itemList[instance.slotID]; 
                //instance.lastItem.itemHeld += -1;
                //if (instance.lastItem.itemHeld == 0)
                //{
                //    instance.myBag.itemList[instance.slotID] = null;
                //}
                //instance.thisItem = toolitem;
                //getItem2();
                instance.myBag.itemList[instance.slotID] = toolitem;
                //instance.myBag.itemList[instance.slotID] = changeBackItem;
                break;
            case 1:
                var toolitem2 = myBag3.itemList3[1];
                instance.myBag3.itemList3[1] = instance.myBag.itemList[instance.slotID];
                //instance.myBag3.itemList3[1] = instance.thisItem;
                //instance.lastItem.itemHeld += -1;
                //if (instance.lastItem.itemHeld == 0)
                //{
                //    instance.myBag.itemList[instance.slotID] = null;
                //}
                instance.myBag.itemList[instance.slotID] = toolitem2;
                //instance.thisItem = toolitem2;
                //getItem2();
                //instance.myBag.itemList[instance.slotID] = changeBackItem;
                break;
            case 2:
                var toolitem3 = myBag3.itemList3[2];
                instance.myBag3.itemList3[2] = instance.myBag.itemList[instance.slotID];
                //instance.myBag3.itemList3[2] = instance.thisItem;
                //instance.lastItem.itemHeld += -1;
                //if (instance.lastItem.itemHeld == 0)
                //{
                //    instance.myBag.itemList[instance.slotID] = null;
                //}
                instance.myBag.itemList[instance.slotID] = toolitem3;
                //instance.thisItem = toolitem3;
                //getItem2();
                //instance.myBag.itemList[instance.slotID] = changeBackItem;
                break;
            default:
                break;
        }
        instance.choosePanel.SetActive(false);
        InventoryManager.RefreshItem1();
        InventoryManager.RefreshItem3();
    }
    //public static void CreateNewItem(Item item)
    //{
    //    Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
    //    newItem.slotImage.sprite = item.itemImage;
    //    newItem.slotItem = item;
    //    newItem.slotNum.text = item.itemHeld.ToString();
    //}

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
            // CreateNewItem(instance.myBag.itemList[i]); 
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid1.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
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

        for (int i = 0; i < instance.myBag2.itemList2.Count; i++)
        {
            // CreateNewItem(instance.myBag.itemList[i]); 
            instance.slots2.Add(Instantiate(instance.emptySlot2));
            instance.slots2[i].transform.SetParent(instance.slotGrid2.transform);
            instance.slots2[i].GetComponent<Slot2>().slotID = i;
            instance.slots2[i].GetComponent<Slot2>().SetupSlot(instance.myBag2.itemList2[i]);
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

        for (int i = 0; i < instance.myBag3.itemList3.Count; i++)
        {
            // CreateNewItem(instance.myBag.itemList[i]); 
            instance.slots3.Add(Instantiate(instance.emptySlot3));
            instance.slots3[i].transform.SetParent(instance.slotGrid3.transform);
            instance.slots3[i].GetComponent<Slot3>().slotID = i;
            instance.slots3[i].GetComponent<Slot3>().SetupSlot(instance.myBag3.itemList3[i]);
        }
    }
}
