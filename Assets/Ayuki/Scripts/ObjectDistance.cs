using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectDistance : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject gameObject2;
    public TextMeshProUGUI text;

    void Update()
    {
        float dis = Vector3.Distance(gameObject.transform.position, gameObject2.transform.position);
        //Debug.Log("‹——£ : " + dis);

        
        text.GetComponent<TextMeshProUGUI>().text = dis.ToString();
        //text.text = string.Format(" ",dis);

    }
}