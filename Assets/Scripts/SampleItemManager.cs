using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SampleItemManager : MonoBehaviour
{
    public SampleItem[] totalItems;
    public static Dictionary<string, SampleItem> itemDictionary = new Dictionary<string, SampleItem>();
    public static Dictionary<string, GameObject> iprefabDictionary = new Dictionary<string, GameObject>();
    public static List<CraftPath> craftList = new List<CraftPath>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < totalItems.Length; i++)
        {
            //Debug.Log(totalItems[i].itemName);
            itemDictionary.Add(totalItems[i].itemName, totalItems[i]);   
        }
        string path = Application.streamingAssetsPath + "/crafts.txt";
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
            {
                //Debug.Log(lines[i]);
                craftList.Add(new CraftPath(lines[i]));
            }
        }
        else
        {
            throw new System.Exception("没有合成表文件！");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static CraftPath getCraftable(string[] ms)
    {
        foreach(CraftPath cp in craftList)
        {
            if (cp.isCraftable(ms))
            {
                return cp;
            }
        }
        return null;
    }
}

public class CraftPath
{
    static char[] tspt = { '\t' };
    static char[] cspt = { ',','，' };

    LinkedList<string> materials = new LinkedList<string>();
    string product = null;

    public CraftPath(string[] ms,string p)
    {
        for(int i = 0; i < ms.Length; i++)
        {
            materials.AddLast(ms[i]);
        }
        product = p;
    }

    public CraftPath(string craftString)
    {
        string[] temp = craftString.Split(tspt);
        string[] ms = temp[0].Split(cspt);
        product = temp[1];
        for (int i = 0; i < ms.Length; i++)
        {
            materials.AddLast(ms[i]);
        }
    }

    public bool isCraftable(string[] current)
    {
        LinkedList<string> ms = new LinkedList<string>(materials);
        for(int i = 0; i < current.Length; i++)
        {
            if( !ms.Remove(current[i]))
            {
                return false;
            }
        }
        if (ms.Count == 0)
        {
            return true;
        }
        return false;
    }
    public bool isCraftable(string[] current,bool fullmatch)
    {
        if (fullmatch)
        {
            return isCraftable(current);
        }
        LinkedList<string> ms = new LinkedList<string>(materials);
        for (int i = 0; i < current.Length; i++)
        {
            if (ms.Remove(current[i]))
            {
                if (ms.Count == 0)
                {
                    return true;
                }
            }
        }
        return false;

    }

    public GameObject getPrefab()
    {
        GameObject prefab = null;
        SampleItemManager.iprefabDictionary.TryGetValue(product,out prefab);
        if (!prefab)
        {
            prefab = Resources.Load<GameObject>($"Prefabs/{product}");
            if (prefab)
            {
                SampleItemManager.iprefabDictionary.Add(product, prefab);
            }
        }
        return prefab;
    }

}
