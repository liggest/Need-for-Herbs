using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warpzone : MonoBehaviour
{
    public Transform WarpPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameCtrl.gc.Warp(collision.transform, WarpPoint.position);
    }
}
