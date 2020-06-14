using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item",menuName = "Items/new item",order =1)]
public class SampleItem : ScriptableObject
{
    

    public string itemName;
    [TextArea]
    public string itemInfo;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
