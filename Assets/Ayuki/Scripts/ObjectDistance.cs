using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectDistance : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject gameObject2;
    public GameObject gameObject3;

    void Update()
    {
        float dis = Vector3.Distance(gameObject.transform.position, gameObject2.transform.position);
        //Debug.Log("‹——£ : " + dis);


        gameObject3.GetComponent<Text>().text ="’n‹…‚Ü‚Å‚Ì‹——£"+ dis.ToString();
        //text.text = string.Format(" ",dis);

    }
}