using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PropItem",menuName ="Inventory/New EquipItem")]
public class EquipItem : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    public int Itemid;

    [TextArea]
    public string itemInfo;

    public bool equip;
}
