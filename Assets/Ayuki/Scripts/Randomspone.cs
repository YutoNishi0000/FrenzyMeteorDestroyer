using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomspone : MonoBehaviour
{
    public GameObject[] PrefabCube = new GameObject[150];
    public GameObject gameObject;
    public GameObject gameObject2;
    GameObject[] obj = new GameObject[150];
    float px,py,gx,gy,dis;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 150; i++)
        {
            float x = Random.Range(-225.0f, 225.0f);
            float y = Random.Range(-225.0f, 225.0f);

            Vector2 v = new Vector2(x, y);

            // プレハブを生成
            obj[i] = Instantiate(PrefabCube[i], v, Quaternion.identity);


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

            /*do
            {
                gameObject.transform.Translate(0, 0, 0);
                gameObject2.transform.Translate(0, 0, 0);
                px = Random.Range(-225.0f, 225.0f);

                py = Random.Range(-225.0f, 225.0f);


                gameObject.transform.Translate(px, py, 0);




                gx = Random.Range(-225, 225);

                gy = Random.Range(-225, 225);


                gameObject2.transform.Translate(gx, gy, 0);


                dis = Vector2.Distance(gameObject.transform.position, gameObject2.transform.position);


            } while (((dis > 500.0f)||(dis<400.0f))||((gx<-225.0f)||(225.0f<gx))|| ((gy < -225.0f) || (225.0f < gy)));*/
            //Transform.Translate(pv(px, py));

        }

    
}

