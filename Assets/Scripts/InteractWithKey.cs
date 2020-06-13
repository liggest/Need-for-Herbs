using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithKey : MonoBehaviour
{
    [Tooltip("这个可交互物体的按键")]
    public KeyCode key;
    [Tooltip("按下按键时触发的函数")]
    public UnityEvent onKeyDown;
    [Tooltip("按键框的脚本")]
    public KeyPresent kp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // 是player
        {
            kp.Show(transform, key, onKeyDown);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // 是player
        {
            if (kp.GetTraget() == transform)
            {
                kp.UnShow();
            }
        }
    }
}
