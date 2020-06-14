using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item3",menuName ="Inventory/New Item3")]
public class Item3 : ScriptableObject {
    public string itemName;
    public Sprite itemEquip;
    public int itemHeld;

    public int Value;

    public float attackValue;
    public float spellPower;
    public float Armor;
    public float spellResistance;
    public float criticalChance;
    public float criticalDamage;
    public float Speed;
    public float healthValue;
    public float healthSteal;
    public int effect;


    [TextArea]
    public string itemInfo;

    public bool equip;
}
