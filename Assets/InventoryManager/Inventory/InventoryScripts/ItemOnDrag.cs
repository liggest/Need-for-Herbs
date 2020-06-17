using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOnDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{

    public Transform originalParent;
    public Inventory myBag;
    public Image slotImage;
    public Text slotNum;
    private int currentItemID;
    private Item item;

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        item = originalParent.GetComponent<Slot>().slotItem;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        InventoryManager.PanelClear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && !eventData.pointerCurrentRaycast.gameObject.CompareTag("prop"))
        {
            //print(eventData.pointerCurrentRaycast.gameObject.name);
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")
            {
              
                myBag.itemList[currentItemID] = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotItem;
                myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = item;
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetComponent<Slot>().slotItem=item;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                //originalParent.GetComponent<Slot>().slotItem=myBag.itemList[currentItemID];
                //ItemList的物品改变
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                if (eventData.pointerCurrentRaycast.gameObject!=originalParent)
                {
                    transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                    transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
                    //eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotItem = item;
                    myBag.itemList[originalParent.GetComponent<Slot>().slotID] = null;
                    myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = item;
                }
                //myBag.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID] = myBag.itemList[currentItemID];
                //if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot>().slotID != currentItemID)
                //{
                //    myBag.itemList[currentItemID] = null;
                //}
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
