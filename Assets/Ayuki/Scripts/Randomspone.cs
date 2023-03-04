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

            // ƒvƒŒƒnƒu‚ð¶¬
            obj[i] = Instantiate(PrefabCube[i], v, Quaternion.identity);


        }

        /*do
        {*/

            px = Random.Range(-225, 225);

            py = Random.Range(-225, 225);


            gameObject.transform.Translate(px, py, 0);




            gx = Random.Range(-225, 225);

            gy = Random.Range(-225, 225);


            gameObject2.transform.Translate(gx, gy, 0);


            dis = Vector3.Distance(gameObject.transform.position, gameObject2.transform.position);


        //} while ((dis > 500)||(dis<400));
        //Transform.Translate(pv(px, py));

    }

    
}

