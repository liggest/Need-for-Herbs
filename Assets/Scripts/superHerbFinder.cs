using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superHerbFinder : MonoBehaviour
{
    public bool isFind = false;
    List<GameObject> superHerbs = null;
    int currentIdx = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (RandomManager.rm.isFinished)
        {
            isFind = true;
        }*/
        if (isFind)
        {
            Transform nsh = GetNearestSuperHerb();
            transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, nsh.position - transform.position));
        }
    }

    private void OnEnable()
    {
        isFind = true;
    }
    private void OnDisable()
    {
        isFind = false;
    }

    Transform GetNearestSuperHerb()
    {
        float distance = float.MaxValue;
        GameObject nearest = null;
        if (superHerbs == null)
        {
            superHerbs = RandomManager.rm.GetSuperHerbs();
        }
        foreach(GameObject sh in superHerbs)
        {
            float tempDistance = Vector3.Distance(sh.transform.position, transform.position);
            if (tempDistance < distance)
            {
                distance = tempDistance;
                nearest = sh;
            }
        }
        return nearest.transform;
    }



}
