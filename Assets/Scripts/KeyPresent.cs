using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class KeyPresent : MonoBehaviour
{
    [Tooltip("按键框在物体上方，这是它和物体的距离")]
    public float topDistance = 2;
    [Tooltip("这是按键框里的字")]
    public Text keyText;
    Transform target;
    UnityEvent keyDown = null;
    KeyCode keycode;
    bool isShow = false;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            SetPos();
            if (Input.GetKeyDown(keycode))
            {
                if (keyDown != null)
                {
                    keyDown.Invoke(); //执行当前记录的UnityEvent的方法
                }
            }
        }
        
    }

    public void Show(Transform t, KeyCode key, UnityEvent keydown)
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        target = t;
        if (target == null) { return; }
        keyDown = keydown;
        keycode = key;
        string keystr = keycode.ToString();
        if (keystr.StartsWith("Alpha"))
        {
            keystr = keystr.Substring(5);
        }
        keyText.text = keystr;
        isShow = true;
        SetPos();
        gameObject.SetActive(true);
        ani.Play("keyPresenterPop");
    }
    void SetPos()
    {
        //Vector3 screenp = Camera.main.WorldToScreenPoint(target.position);
        //transform.position = new Vector3(screenp.x, screenp.y + topDistance, -1);
        transform.position = target.position + target.up * topDistance;
    }

    public void UnShow()
    {
        ani.Play("keyPresenterPopOut");
    }
    public Transform GetTraget()
    {
        return target;
    }
    void DisActive()
    {
        keyDown = null;
        isShow = false;
        gameObject.SetActive(false);
    }
}
