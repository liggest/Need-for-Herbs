using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item2",menuName ="Inventory/New Item2")]
public class Item2 : ScriptableObject {
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;

    public int Value;
    public int effect;


    [TextArea]
    public string itemInfo;

    public bool equip;
}
