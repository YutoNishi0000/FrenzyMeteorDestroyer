using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketController : Actor
{
    [SerializeField] private GameObject rocketPref; //rocketのプレハブ
    [SerializeField] private float generateTime;    //何秒ごとにrocketを生成するか
    [SerializeField] private int numRockets;        //ゲーム開始時あらかじめ何個のrocketを生成するか
    private List<GameObject> rocketList;            //生成したrocketを格納するためのリスト
    private float time;
    

    // Start is called before the first frame update
    void Start()
    {
        rocketList = new List<GameObject>();

        for(int i = 0; i < numRockets; i++)
        {
            GameObject clone = Instantiate(rocketPref, transform.position, Quaternion.identity);
            clone.SetActive(false);
            rocketList.Add(clone);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > generateTime)
        {
            LaunchRocket();
            time = 0;
        }
    }

    void LaunchRocket()
    {
        GameObject rocketObj = GetRocket();
        rocketObj.SetActive(true);
        rocketObj.transform.position = transform.position;
        Vector3 dir = Instance.transform.position - transform.position;
        rocketObj.GetComponent<Rigidbody2D>().AddForce((Vector2)dir, ForceMode2D.Impulse);
    }

    GameObject GetRocket()
    {
        for(int i = 0; i < numRockets; i++)
        {
            if (rocketList[i].activeInHierarchy == false)
            {
                return rocketList[i];
            }
        }

        return null;
    }
}
