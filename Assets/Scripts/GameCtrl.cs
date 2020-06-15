using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl gc;
    [Tooltip("以秒为单位的关卡时间")]
    public float levelTime = 120;
    public Text timeText;
    bool isCountDown = false;
    bool isLevelEnd = false;
    float gametime = -1;
    float second = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (gc != null)
        {
            Destroy(this);
        }
        gc = this;
        ResetTimer();
        StartCountDown();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountDown)
        {
            second += Time.deltaTime;
            if (second >= 1)
            {
                second = 0;
                gametime -= 1;
                if (gametime <= 0)
                {
                    StopCountDown();
                    LevelEnd();
                }
            }
        }
        
    }

    private void OnGUI()
    {
        if (isLevelEnd)
        {
            timeText.text = "时间到";
            return;
        }
        int M = (int)(gametime / 60);
        float S = gametime % 60;
        timeText.text = string.Format("{0:00}:{1:00}", M, S);
    }

    public void StartCountDown()
    {
        isCountDown = true;
    }
    public void StopCountDown()
    {
        isCountDown = false;
    }
    public void ResetTimer()
    {
        gametime = levelTime;
        second = 0;
    }

    public void LevelEnd()
    {
        Time.timeScale = 0;
        isLevelEnd = true;
    }
}
