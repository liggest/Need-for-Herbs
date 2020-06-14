using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItemPrefab : MonoBehaviour
{
    public SampleItem item;
    // Start is called before the first frame update
    void Start()
    {
        transform.name = item.itemName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
