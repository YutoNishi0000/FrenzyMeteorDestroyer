using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomspone : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ofsEnemy;
    private Sprite sprite;

    private GameObject[] PrefabCube = new GameObject[500];
    public GameObject gameObject;
    public GameObject gameObject2;
    GameObject[] obj = new GameObject[500];
    private GameObject children;
    private GameObject enemy;
    float px,py,gx,gy,dis;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            float x = Random.Range(-225.0f, 225.0f);
            float y = Random.Range(-225.0f, 225.0f);

            Vector3 v = new Vector3(x, y,4);
            PrefabCube[i] = ofsEnemy[Random.Range(0, 2)];
            // プレハブを生成
            obj[i] = Instantiate(PrefabCube[i], v, Quaternion.identity);

            
  
            Debug.Log(children);
            //obj[i] = ofsEnemy[Random.Range(0, 2)];
            //obj[i] = enemy;


        }
        //ランダムに6種類のパターンを選出する
        px = Random.Range(0, 3);
        switch (px) {
            
            case 0:
                gameObject2.transform.Translate(-200, -200, 2);
                gameObject.transform.Translate(200, 200, 2);
                break;
            case 1:
                gameObject2.transform.Translate(0, -200, 2);
                gameObject.transform.Translate(0, 200, 2);
                break;
            case 2:
                gameObject2.transform.Translate(200, -200, 2);
                gameObject.transform.Translate(-200, 200, 2);
                break;
                    }

            

        }

    
}

