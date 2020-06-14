using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SampleItemDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    Transform myCanvas;
    RectTransform imgt;
    Vector2 gap;
    Vector3 oldScale;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Vector2 imgpos = Camera.main.WorldToScreenPoint(imgt.position);
        Vector2 imgpos = imgt.position;
        gap = imgpos - eventData.pressPosition;
        SetImgPos(eventData.position);
        transform.localScale = oldScale * 1.2f;
        transform.SetParent(myCanvas, false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetImgPos(eventData.position);
    }

    public void OnDrop(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(eventData.position, Vector2.down, 2);
        //Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Dropable")
            {
                transform.SetParent(hit.collider.transform, true);
            }
                
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = oldScale;
        //SetImgPos(eventData.position);
    }

    void SetImgPos( Vector2 mousePos )
    {
        //imgt.position = Camera.main.ScreenToWorldPoint(mousePos + gap);
        imgt.position = mousePos + gap;
    }

    // Start is called before the first frame update
    void Start()
    {
        myCanvas = transform.parent;
        imgt = GetComponent<RectTransform>();
        oldScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
