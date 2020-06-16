using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PropItem",menuName ="Inventory/New PropItem")]
public class PropItem : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    public int Itemid;
    //public List<Sprite> materialimg;
    public Item material1;
    public Item material2;
    public int material1num;
    public int material2num;
    //public int itemHeld;

    //public int Value;
    //public int effect;


    [TextArea]
    public string itemInfo;

    public bool equip;
}
