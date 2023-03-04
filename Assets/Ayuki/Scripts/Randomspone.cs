using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomspone : MonoBehaviour
{
    [SerializeField]
    private Sprite[] ofsImage;
    private Sprite sprite;

    public GameObject[] PrefabCube = new GameObject[500];
    public GameObject gameObject;
    public GameObject gameObject2;
    GameObject[] obj = new GameObject[500];
    private GameObject children;
    float px,py,gx,gy,dis;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 500; i++)
        {
            float x = Random.Range(-225.0f, 225.0f);
            float y = Random.Range(-225.0f, 225.0f);

            Vector2 v = new Vector2(x, y);

            // プレハブを生成
            obj[i] = Instantiate(PrefabCube[i], v, Quaternion.identity);

            children = obj[i].transform.Find("Square").gameObject;
         
            Debug.Log(children);
            sprite = ofsImage[Random.Range(0, 3)];
            children.GetComponent<SpriteRenderer>().sprite = sprite;
            
        }
        //ランダムに6種類のパターンを選出する
        px = Random.Range(0, 5);
        switch (px) {
            case 0:
                gameObject.transform.Translate(-200, -200, 0);
                gameObject2.transform.Translate(200, 200, 0);
                break;
            case 1:
                gameObject.transform.Translate(0, -200, 0);
                gameObject2.transform.Translate(0, 200, 0);
                break;
            case 2:
                gameObject.transform.Translate(200, -200, 0);
                gameObject2.transform.Translate(-200, 200, 0);
                break;
            case 3:
                gameObject2.transform.Translate(-200, -200, 0);
                gameObject.transform.Translate(200, 200, 0);
                break;
            case 4:
                gameObject2.transform.Translate(0, -200, 0);
                gameObject.transform.Translate(0, 200, 0);
                break;
            case 5:
                gameObject2.transform.Translate(200, -200, 0);
                gameObject.transform.Translate(-200, 200, 0);
                break;
                    }

            

        }

    
}

