﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName ="Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
    public List<PropItem> itemList2 = new List<PropItem>();
    public List<EquipItem> itemList3 = new List<EquipItem>();
}
 