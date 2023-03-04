using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomspone : MonoBehaviour
{
    public GameObject[] PrefabCube = new GameObject[150];
    public GameObject gameObject;
    public GameObject gameObject2;
    GameObject[] obj = new GameObject[150];
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
        

        float px = Random.Range(-225.0f, 225.0f);
        
        float py = Random.Range(-225.0f, 225.0f);
       

        Vector2 pv = new Vector2(px, py);
        gameObject.transform.Translate(px, py, 0.0f);

        float gx = Random.Range(-225.0f, 225.0f);

        float gy = Random.Range(-225.0f, 225.0f);


        Vector2 gv = new Vector2(gx, gy);
        gameObject.transform.Translate(gx, gy, 0.0f);
        //Transform.Translate(pv(px, py));

    }

    
}

