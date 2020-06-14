using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag2 : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot2>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && !eventData.pointerCurrentRaycast.gameObject.CompareTag("use"))
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
                var temp = myBag.itemList2[currentItemID];
                myBag.itemList2[currentItemID] = myBag.itemList2[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot2>().slotID];
                myBag.itemList2[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot2>().slotID] = temp;
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                //ItemList的物品改变
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            else if (eventData.pointerCurrentRaycast.gameObject.name == "slot2(Clone)")
            {
                myBag.itemList2[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot2>().slotID] = myBag.itemList2[currentItemID];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot2>().slotID != currentItemID)
                {
                    myBag.itemList2[currentItemID] = null;
                }
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
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
