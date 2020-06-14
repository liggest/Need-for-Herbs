using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItemCraft : MonoBehaviour
{
    int oldCount = 0;
    Vector3 center;
    string[] currents;
    GameObject currentPrefab;
    bool isCrafting = false;


    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        center = rt.position;
        currents = new string[0];
    }

    // Update is called once per frame
    void Update()
    {
        int newCount = transform.childCount;
        if (newCount > 0)
        {
            for(int i=0;i< newCount; i++)
            {
                
                Transform child = transform.GetChild(i);
                float distance = Vector2.Distance(child.position, center);
                if (!isCrafting)
                {
                    if (distance > 80)
                    {
                        Vector2 cv = center - child.position;
                        child.position = Vector2.SmoothDamp(child.position, center, ref cv, 0.3f);
                    }
                    else
                    {
                        child.RotateAround(center, Vector3.forward, 30.0f * Time.deltaTime);
                        child.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                else
                {
                    if (distance < 2)
                    {
                        Destroy(child.gameObject);
                    }
                    else
                    {
                        Vector2 cv = center - child.position;
                        child.position = Vector2.SmoothDamp(child.position, center, ref cv, 0.1f);
                    }

                }
            }
            if (newCount != oldCount && !isCrafting)
            {
                currents = new string[newCount];
                for(int i=0;i< newCount; i++)
                {
                    currents[i] = transform.GetChild(i).name;
                }
                CraftPath cp = SampleItemManager.getCraftable(currents);
                if (cp!=null)
                {
                    currentPrefab = cp.getPrefab();
                    if (currentPrefab)
                    {
                        isCrafting = true;
                    }
                }
            }
        }else if (isCrafting)
        {
            Instantiate<GameObject>(currentPrefab, center, Quaternion.Euler(0, 0, 0), transform.parent);
            isCrafting = false;
            currentPrefab = null;
        }
        oldCount = newCount;
    }
}
