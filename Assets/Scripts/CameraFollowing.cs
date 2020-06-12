using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [Tooltip("相机追踪的对象")]
    public Transform target;
    [Tooltip("相机追上对象的时间")]
    public float smoothTime = 0.3f;
    [Tooltip("对象离开画面中心多远，相机开始追踪")]
    public float outRadius = 4.5f;
    [Tooltip("对象距离画面中心多近，相机完全停止追踪")]
    public float inRaduis = 0.03f;
    bool isMove = false;
    Vector2 cv = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 center = Camera.main.transform.position;
        Vector2 tar = target.position;
        float distance = Vector2.Distance(center, tar);
        if (distance > outRadius)
        {
            isMove = true;
        }
        else if (distance < inRaduis && isMove)
        {
            //Debug.Log("停了");
            isMove = false;
        }
        if (isMove)
        {
            Vector2 delta = Vector2.SmoothDamp(center, tar, ref cv, smoothTime);
            float cameraZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(delta.x, delta.y, cameraZ);
        }
    }
}
