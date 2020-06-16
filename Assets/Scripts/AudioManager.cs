using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AM; //提供单例
    [Tooltip("播放音乐用的AudioSource")]
    public AudioSource musicAS;
    [Tooltip("播放音效用的AudioSource")]
    public AudioSource soundAS;
    [Tooltip("如果非空，则场景加载后便尝试播放这个名字的BGM")]
    public string BGMname = "";
    [Tooltip("音效集")]
    public AudioClip[] sounds;
    public ResourceRequest rr;
    // Start is called before the first frame update
    private void Awake()
    {
        if (AM != null)
        {
            Destroy(this);
        }
        AM = this;
    }

    void Start()
    {
        if (BGMname != "")
        {
            try
            {
                AM.PlayMusic($"Audios/{BGMname}");
            }
            catch
            {
                Debug.Log($"读取BGM：{BGMname} 失败，请检查Resources/Audios/下是否有相应名字的音频");
            }
        }
        //AM.PlayMusic("Audios/Monkeys Spinning Monkeys");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.AM.PlaySound(1);
        }else if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {
            AudioManager.AM.PlaySound(0, true); //true的话，上个音效没播完就不会播放
        }else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AudioManager.AM.PlaySound(2);
        }
        */
    }

    public void PlayMusic(string name)
    {
        StartCoroutine(MusicLoader(name));
    }

    IEnumerator MusicLoader(string name)
    {
        rr = Resources.LoadAsync<AudioClip>(name);
        yield return rr;

        AudioClip music = rr.asset as AudioClip;
        if (!music)
        {
            Debug.Log($"音乐{name}加载失败");
            yield break;
        }
        if (musicAS.isPlaying)
        {
            musicAS.Stop();
        }
        musicAS.clip = music;
        musicAS.Play();
    }
    public void PlaySound(int idx)
    {
        PlaySound(idx, false);

    }
    public void PlaySound(int idx,bool checkPlay)
    {
        if(idx <0 || idx >= sounds.Length)
        {
            Debug.Log($"{idx}号音效不存在");
            return;
        }
        AudioClip sound = sounds.GetValue(idx) as AudioClip;
        if (!sound)
        {
            Debug.Log($"{idx}号音效不存在");
            return;
        }
        if(checkPlay && soundAS.isPlaying)
        {
            return;
        }
        soundAS.PlayOneShot(sound);
    }
}
