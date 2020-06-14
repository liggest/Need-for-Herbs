using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBag : MonoBehaviour
{

    public int bagNum;
    public GameObject Bag1;
    public GameObject Bag2;
    public Text textBtn1;
    public Text textBtn2;

    private void OnEnable()
    {
        ChangeBag1();
    }
    public void ChangeBag1()
    {
        bagNum = 0;
        textBtn1.color = new Color32(255, 255, 255, 255);
        textBtn2.color = new Color32(255, 255, 255, 120);
        ShowBag();
    }
    public void ChangeBag2()
    {
        bagNum = 1;
        textBtn2.color = new Color32(255, 255, 255, 255);
        textBtn1.color = new Color32(255, 255, 255, 120);
        ShowBag();
    }
    public void ShowBag()
    {
        if (bagNum == 0)
        {
            Bag1.SetActive(true);
            Bag2.SetActive(false);
        }
        else if (bagNum == 1)
        {
            Bag1.SetActive(false);
            Bag2.SetActive(true);
        }
    }
}
