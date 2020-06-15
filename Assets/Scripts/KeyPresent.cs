using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class KeyPresent : MonoBehaviour
{
    [Tooltip("按键框在物体上方，这是它和物体的距离")]
    public float topDistance = 2;
    [Tooltip("提示开启时提示音的序号")]
    public int openSound = -1;
    [Tooltip("提示关闭时提示音的序号")]
    public int closeSound = -1;
    //[Tooltip("这是按键框里的字")]
    float topdistance = 2;
    Text keyText;
    RectTransform bgRect;
    Transform target;
    UnityEvent keyDown = null;
    KeyCode keycode;
    bool isShow = false;
    int bgFit = -1;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        topdistance = topDistance;
        ani = GetComponent<Animator>();
        Transform bg = transform.GetChild(0);
        bgRect = bg.GetComponent<RectTransform>();
        keyText = bg.GetComponentInChildren<Text>();
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            if (bgFit >= 0)
            {
                bgRect.sizeDelta = keyText.rectTransform.sizeDelta + new Vector2(6, 2);
                bgFit += 1;
                if (bgFit > 1)
                {
                    bgFit = -1;
                }
            }
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

    public void SetTopdistance(float value)
    {
        topdistance = value;
    }

    public void Show(Transform t,string info, KeyCode key, UnityEvent keydown)
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
        target = t;
        if (target == null) { return; }
        keyDown = keydown;
        keycode = key;
        /*
        string keystr = keycode.ToString();
        if (keystr.StartsWith("Alpha"))
        {
            keystr = keystr.Substring(5);
        }
        keyText.text = keystr;*/
        keyText.text = info;
        bgFit = 0;
        isShow = true;
        SetPos();
        gameObject.SetActive(true);
        if(openSound>=0 && AudioManager.AM)
        {
            AudioManager.AM.PlaySound(openSound);
        }
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
        if (closeSound>=0 && AudioManager.AM)
        {
            AudioManager.AM.PlaySound(closeSound);
        }
        ani.Play("keyPresenterPopOut");
    }
    public Transform GetTraget()
    {
        return target;
    }
    void DisActive()
    {
        topdistance = topDistance;
        keyDown = null;
        isShow = false;
        gameObject.SetActive(false);
    }
}
