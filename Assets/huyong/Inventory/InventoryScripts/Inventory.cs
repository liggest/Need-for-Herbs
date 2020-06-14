using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName ="Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
    public List<Item2> itemList2 = new List<Item2>();
    public List<Item> itemList3 = new List<Item>();
}
 