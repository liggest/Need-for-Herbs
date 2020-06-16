﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public int herbsCount = 45;
    public int materialsCount = 30;
    public GameObject[] herbsPrefabs;
    public GameObject[] materialsPrefabs;
    public float groundLine = -4.0f;
    public float herbsPOnGround = 0.8f;
    public float materialsPOnGround = 0.3f;
    public Transform Player;
    public Transform[] PlayerInitPoints;

    BoxCollider2D[] plane2box;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");
        plane2box = new BoxCollider2D[planes.Length];

        Transform initPoint = PlayerInitPoints[GetRandomIdx(PlayerInitPoints.Length)];
        GameCtrl.gc.Warp(Player, initPoint.position, true);
        Player.gameObject.SetActive(false);

        for(int i = 0; i < herbsCount; i++)
        {
            GenerateHerb(planes);
        }
        for (int i = 0; i < materialsCount; i++)
        {
            GenerateMaterial(planes);
        }

        Player.gameObject.SetActive(true);
        GameCtrl.gc.StartCountDown();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject GetMaterial()
    {
        return materialsPrefabs[ GetRandomIdx(materialsPrefabs.Length) ];
    }
    GameObject GetHerb()
    {
        return herbsPrefabs[ GetRandomIdx(herbsPrefabs.Length) ];
    }
    int GetRandomIdx(int len)
    {
        return Random.Range(0, len);
    }
    BoxCollider2D GetBoxCollider(int idx,GameObject[] planes)
    {
        if (!plane2box[idx])
        {
            plane2box[idx] = planes[idx].GetComponent<BoxCollider2D>();
            
        }
        return plane2box[idx];
    }

    Vector3 GetPosFromCollider(BoxCollider2D bc,Transform pt)
    {
        Vector2 result = pt.position;
        float bcx = bc.size.x;
        float bcy = bc.size.y;
        return result+bc.offset+ new Vector2(bcx * Random.value - bcx / 2.0f, bcy / 2.0f);
    }
    void GenerateMaterial(GameObject[] planes)
    {
        bool isGenerated = false;
        GameObject m = GetMaterial();
        //int idx = 0;
        Vector3 pos = new Vector3();
        while (!isGenerated)
        {
            int pidx = GetRandomIdx(planes.Length);
            Transform pt = planes[pidx].transform;
            if (GroundTest(materialsPOnGround, pt.position.y))
            {
                BoxCollider2D bc = GetBoxCollider(pidx, planes);
                if (!bc) { continue; }
                pos = GetPosFromCollider(bc, pt);
                isGenerated = true;
            }
        }
        Instantiate<GameObject>(m, pos, Quaternion.Euler(0, 0, 0));
    }

    void GenerateHerb(GameObject[] planes)
    {
        bool isGenerated = false;
        GameObject h = GetHerb();
        //int idx = 0;
        Vector3 pos = new Vector3();
        while (!isGenerated)
        {
            int pidx = GetRandomIdx(planes.Length);
            Transform pt = planes[pidx].transform;
            if (GroundTest(herbsPOnGround, pt.position.y))
            {
                BoxCollider2D bc = GetBoxCollider(pidx, planes);
                pos = GetPosFromCollider(bc, pt);
                isGenerated = true;
            }
        }
        Instantiate<GameObject>(h, pos, Quaternion.Euler(0, 0, 0));
    }

    bool GroundTest(float groundP, float y)
    {
        float rval = Random.value;
        if (y >= groundLine)
        {
            if (groundP >= rval)
            {
                return true;
            }
        }
        else
        {
            if(1-groundP >= rval)
            {
                return true;
            }
        }
        return false;
    }

}