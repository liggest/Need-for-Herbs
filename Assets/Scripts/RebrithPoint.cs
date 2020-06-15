using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebrithPoint : MonoBehaviour
{
    public bool isWork = false;
    //Vector3 point;
    // Start is called before the first frame update
    void Start()
    {
        //point = transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8) // 如果是Player
        {
            if (!isWork)
            {
                //Debug.Log($"激活{point}");
                isWork = true;
                GameCtrl.gc.AddWorkPoint(transform.position);
            }
        }
    }
}
