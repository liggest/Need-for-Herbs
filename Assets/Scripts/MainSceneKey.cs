using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainSceneKey : MonoBehaviour
{
    [Tooltip("交互的提示文本"),TextArea]
    public string hintInfo;

    [Tooltip("药材收集完成提示文本"), TextArea]
    public string hintInfosuccess;
    [Tooltip("这个可交互物体的按键")]
    public KeyCode key;
    [Tooltip("按下按键时触发的函数")]
    public UnityEvent onKeyDown;
    [Tooltip("按键框的脚本")]
    public KeyPresent kp;

    bool istrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (istrigger)
        {
            if (GameCtrl.gc.isfinished && Input.GetKeyDown(KeyCode.Y))
            {
                GameCtrl.gc.finish();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // 是player
        {
            istrigger = true;
            if (GameCtrl.gc.isfinished)
            {
                kp.Show(transform, hintInfosuccess, key, onKeyDown);
            }
            else
            {
                kp.Show(transform, hintInfo, key, onKeyDown);
            }  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // 是player
        {
            istrigger = false;
            if (kp.GetTraget() == transform)
            {
                kp.UnShow();
            }
        }
    }

    public void TestFunc()
    {
        Debug.Log("按键了" + key.ToString());
    }
}
