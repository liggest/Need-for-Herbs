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
    public float inRadius = 0.03f;
    [Tooltip("相机单次移动距离多近，相机完全停止追踪")]
    public float deltaRadius = 0.01f;
    [Tooltip("场景边框，X、Y对应左下角位置，W、H对应场景大小")]
    public Rect sceneRect;
    bool isMove = false;
    Vector2 cv = new Vector2();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 center = Camera.main.transform.position;
        Vector2 tar = target.position;
        float distance = Vector2.Distance(center, tar);
        if (distance > outRadius)
        {
            isMove = true;
        }
        if (isMove)
        {
            Vector2 delta = Vector2.SmoothDamp(center, tar, ref cv, smoothTime);
            float cameraZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(delta.x, delta.y, cameraZ);
            RestrictCameraPos();
            float deltaDistance = Vector2.Distance(Camera.main.transform.position, center);
            //Debug.Log(deltaDistance);
            if (distance < inRadius || deltaDistance < deltaRadius)
            {
                //Debug.Log("停了");
                isMove = false;
            }
            
        }
        */
    }

    private void FixedUpdate()
    {
        Vector2 center = Camera.main.transform.position;
        Vector2 tar = target.position;
        float distance = Vector2.Distance(center, tar);
        if (distance > outRadius)
        {
            isMove = true;
        }
        if (isMove)
        {
            Vector2 delta = Vector2.SmoothDamp(center, tar, ref cv, smoothTime);
            float cameraZ = Camera.main.transform.position.z;
            Camera.main.transform.position = new Vector3(delta.x, delta.y, cameraZ);
            RestrictCameraPos();
            float deltaDistance = Vector2.Distance(Camera.main.transform.position, center);
            //Debug.Log(deltaDistance);
            if (distance < inRadius || deltaDistance < deltaRadius)
            {
                //Debug.Log("停了");
                isMove = false;
            }

        }
    }

    Rect GetCameraRect()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        Vector2 center = Camera.main.transform.position;
        float left = center.x - width / 2;
        float buttom = center.y - height / 2;
        return new Rect(left, buttom, width, height);
    }

    void RestrictCameraPos()
    {
        Rect cr = GetCameraRect();
        if (cr.x < sceneRect.x)
        {
            Camera.main.transform.position += new Vector3(sceneRect.x - cr.x, 0, 0);
        }
        if (cr.y < sceneRect.y)
        {
            Camera.main.transform.position += new Vector3(0, sceneRect.y - cr.y, 0);
        }
        if(cr.xMax > sceneRect.xMax)
        {
            Camera.main.transform.position += new Vector3(sceneRect.xMax - cr.xMax, 0, 0);
        }
        if(cr.yMax > sceneRect.yMax)
        {
            Camera.main.transform.position += new Vector3(0, sceneRect.yMax - cr.yMax, 0);
        }
    }
}
