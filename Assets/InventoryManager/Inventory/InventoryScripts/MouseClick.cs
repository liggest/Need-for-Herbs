using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;

    public GameObject panel1;


    private void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));
        middleClick.AddListener(new UnityAction(ButtonMiddleClick));
        rightClick.AddListener(new UnityAction(ButtonRightClick));
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }


    private void ButtonLeftClick()
    {
        //print("Button Left Click");
    }

    private void ButtonMiddleClick()
    {
        //Debug.Log("Button Middle Click");
    }

    private void ButtonRightClick()
    {
        //Debug.Log("Button Right Click");
    }

}
