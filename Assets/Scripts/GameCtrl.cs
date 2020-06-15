using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtrl : MonoBehaviour
{
    public static GameCtrl gc;
    [Tooltip("以秒为单位的关卡时间")]
    public float levelTime = 120;
    public Text timeText;
    public Text hintText;
    //public RebrithPoint[] rebriths;
    bool isCountDown = false;
    bool isLevelEnd = false;
    float gametime = -1;
    float second = 0;
    LinkedList<Vector3> workingRebriths = new LinkedList<Vector3>();
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

    public void Warp(Transform target,Vector3 point)
    {
        target.position = point;
        if (target.gameObject.layer == 8) // 如果是player
        {
            float cameraZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(point.x, point.y, cameraZ);
        }
    }

    public void AddWorkPoint( Vector3 point )
    {
        if (!workingRebriths.Contains(point))
        {
            workingRebriths.AddLast(point);
        }
    }

    public Vector3 GetRebrithPoint(Vector3 deathPoint)
    {
        float minDistance = float.MaxValue;
        Vector3 nearest = new Vector3();
        foreach(Vector3 rp in workingRebriths)
        {
            float disstance = Vector3.Distance(rp, deathPoint);
            if (disstance < minDistance)
            {
                minDistance = disstance;
                nearest = rp;
            }
        }
        return nearest;
    }

    public void LoadScene(string name)
    {
        Text progress = hintText;
        StartCoroutine(SceneLoading(name, progress));
    }

    IEnumerator SceneLoading(string name,Text progress)
    {
        bool ptext = false;
        if (progress)
        {
            ptext = true;
        }

        AsyncOperation ap = SceneManager.LoadSceneAsync(name);
        ap.allowSceneActivation = true;
        while (ap.progress < 1.0f)
        {
            if (ptext)
            {
                int pgs = (int)(ap.progress * 100);
                progress.text = $"{pgs}%";
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
