using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
    public static RandomManager rm;

    public int herbsCount = 45;
    public GameObject[] herbsPrefabs;
    public int materialsCount = 30;
    public GameObject[] materialsPrefabs;
    public int superHerbsCount = 3;
    public GameObject[] superHerbsPrefabs;
    
    public float groundLine = -4.0f;
    public float herbsPOnGround = 0.8f;
    public float materialsPOnGround = 0.3f;

    public Transform Player;
    public Transform[] PlayerInitPoints;

    public bool isFinished = false;

    BoxCollider2D[] plane2box;
    List<GameObject> superherbs = new List<GameObject>();
    void Awake()
    {
        if (rm != null)
        {
            Destroy(this);
        }
        rm = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] planes = GameObject.FindGameObjectsWithTag("Plane");
        plane2box = new BoxCollider2D[planes.Length];

        Transform initPoint = PlayerInitPoints[GetRandomIdx(PlayerInitPoints.Length)];
        GameCtrl.gc.Warp(Player, initPoint.position, true);
        Player.gameObject.SetActive(false);

        for(int i = 0; i < superHerbsCount; i++)
        {
            GenerateSuperHerb(planes);
        }
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
        isFinished = true;
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
    GameObject GetSuperHerb()
    {
        return superHerbsPrefabs[ GetRandomIdx(superHerbsPrefabs.Length) ];
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

    void GenerateSuperHerb(GameObject[] planes)
    {
        GameObject h = GetSuperHerb();
        Vector3 pos = new Vector3();
        int pidx = GetRandomIdx(planes.Length);
        Transform pt = planes[pidx].transform;
        BoxCollider2D bc = GetBoxCollider(pidx, planes);
        pos = GetPosFromCollider(bc, pt);
        GameObject sh = Instantiate<GameObject>(h, pos, Quaternion.Euler(0, 0, 0));
        superherbs.Add(sh);
    }

    public List<GameObject> GetSuperHerbs()
    {
        return superherbs;
    }
    public void RemoveSuperHerbs(GameObject target)
    {
        superherbs.Remove(target);
    }
}
