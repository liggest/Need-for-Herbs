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
    [Tooltip("是否在场景加载后便开始计时")]
    public bool startCountdown = false;
    [Tooltip("计时用的文字")]
    public Text timeText;
    [Tooltip("keyPresenter中的提示文字")]
    public Text hintText;
    [Tooltip("游戏结束的图片")]
    public Image endImg;
    [Tooltip("玩家")]
    public Transform Player;
    [Header("这里记录了Prefab需要的一些属性")]
    public GameObject BagFullPanel;
    public Text BagFullPanelTips;
    public Slider slider;

    //public RebrithPoint[] rebriths;
    bool isCountDown = false;
    bool isLevelEnd = false;
    bool isAnyKey = false;
    float gametime = -1;
    float second = 0;
    Vector3 initPoint;
    LinkedList<Vector3> workingRebriths = new LinkedList<Vector3>();
    // Start is called before the first frame update
    void Awake()
    {
        if (gc != null)
        {
            Destroy(this);
        }
        gc = this;
    }
    void Start()
    {
        initPoint = Player.position;
        AddWorkPoint(initPoint);

        ResetTimer();
        if (startCountdown)
        {
            StartCountDown();
        }
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
        if (isLevelEnd)
        {
            if (isAnyKey && Input.anyKeyDown)
            {
                LoadScene("Tutorial");
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
        Player.SendMessage("LevelEnd", SendMessageOptions.DontRequireReceiver);
        //Time.timeScale = 0;
        isLevelEnd = true;
        StartCoroutine(LevelEndCortine());
    }
    IEnumerator LevelEndCortine()
    {
        endImg.gameObject.SetActive(true);
        float er = endImg.color.r;
        float eg = endImg.color.g;
        float eb = endImg.color.b;
        endImg.color = new Color(er, eg, eb, 0);
        float ea = endImg.color.a;
        while (ea < 1)
        {
            Debug.Log(ea);
            endImg.color = new Color(er, eg, eb, ea + 0.7f * Time.deltaTime);
            ea = endImg.color.a;
            if (ea > 1)
            {
                endImg.color = new Color(er, eg, eb, 1);
            }
            yield return null;
        }
        isAnyKey = true;

    }

    public void Warp(Transform target,Vector3 point)
    {
        Warp(target,point,false);
    }

    public void Warp(Transform target,Vector3 point,bool cameraMove)
    {
        target.position = point;
        if (cameraMove)
        {
            if (target.gameObject.layer == 8) // 如果是player
            {
                float cameraZ = Camera.main.transform.position.z;
                Camera.main.transform.position = new Vector3(point.x, point.y, cameraZ);
            }
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
