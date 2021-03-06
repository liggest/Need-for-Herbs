﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class MainMenuGUI : MonoBehaviour {
 
    public AudioClip beep;
    public AudioClip mainmusic;
    public GUISkin menuSkin;
    public Rect menuArea;
    public Rect playButton;
    public Rect instructionsButton;
    public Rect quitButton;
    public Rect instructions;
    public Rect quitttButton;
    Rect menuAreaNormalized;
    string menuPage = "main";
	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().PlayOneShot(mainmusic);
        menuAreaNormalized =
            new Rect(menuArea.x * Screen.width - (menuArea.width * 0.5f), menuArea.y * Screen.height - (menuArea.height * 0.5f), menuArea.width, menuArea.height);
    }
    void OnGUI()
    {
        GUI.skin = menuSkin;
        GUI.BeginGroup(menuAreaNormalized);
        if (menuPage == "main")
        {
            if (GUI.Button(new Rect(playButton), "开始游戏"))
            {
                StartCoroutine("ButtonAction", "Tutorial");
                //SceneManager.LoadScene("Tutorial");
                //Application.LoadLevel("Tutorial");
                // ——网上说这个方法过时，应该用索引的，不再适用,但这个还是跑通了哈哈哈
                //按下按钮“play”，跳转进场景“start222”
                //最后还是改用SceneManager.LoadScene
            }
            if (GUI.Button(new Rect(instructionsButton), "操作说明"))
            {
                //GetComponent<AudioSource>().PlayOneShot(beep);
                menuPage = "instructions";
 
            }
            if (GUI.Button(new Rect(quitButton), "退出"))
            {
                StartCoroutine("ButtonAction", "quit");
            }
        }
        else if(menuPage=="instructions")
        {
            GUI.Label(new Rect(instructions), "十字键:移动\nB:背包\nY:采集\nG:冲刺");
            //GUI.Label(new Rect(200, 40, 100, 30), "aaaaa" );
            if(GUI.Button(new Rect(quitttButton),"返回"))
            {
                //GetComponent<AudioSource>().PlayOneShot(beep);
                menuPage = "main";
            }
        }
        GUI.EndGroup();
    }
    IEnumerator ButtonAction(string levelName)
    {
        //GetComponent<AudioSource>().PlayOneShot(beep);
        yield return new WaitForSeconds(0.35f);
        
        if(levelName!="quit")
        {
            SceneManager.LoadScene(levelName);
            //Application.LoadLevel(levelName);
        }
        else
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}