using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Movebag : MonoBehaviour, IDragHandler
{
    RectTransform currentRect;

    public void OnDrag(PointerEventData eventData)
    {
        currentRect.anchoredPosition += eventData.delta;
    }

    private void Awake()
    {
        currentRect = GetComponent<RectTransform>();
    }
}
