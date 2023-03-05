using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour
{
    [SerializeField]
    private AudioSource source; //スピーカー・CDプレイヤー

    [SerializeField]
    private AudioClip clip1; //音源データ1

    [SerializeField]
    private AudioClip clip2;

    [SerializeField]
    private Sprite spriteearth1;

    [SerializeField]
    private Sprite spriteearth2;

    [SerializeField]
    private Sprite spriteearth3;

    [SerializeField]
    private GameObject earth;

    [SerializeField]
    private GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        if(MeteoriteController.isClear)
        {
            if(MeteoriteController.lustHP==3)
            {
                gameObject.GetComponent<Text>().text = "おめでとう!!地球は消えてしまったよ!!";
                earth.SetActive(false);
                source.clip = clip1;
            }
            else if(MeteoriteController.lustHP == 2)
            {
                gameObject.GetComponent<Text>().text = "ナイス!!地球はほぼ壊滅したよ!!";
                earth.GetComponent<SpriteRenderer>().sprite = spriteearth2;
                source.clip = clip2;
            }
            else if (MeteoriteController.lustHP == 1)
            {
                gameObject.GetComponent<Text>().text = "やったね!!生命は絶滅したよ!!";
                earth.GetComponent<SpriteRenderer>().sprite = spriteearth3;
                source.clip = clip2;
            }
                
        }
        else{
            source.clip = clip2;
            gameObject.GetComponent<Text>().text ="残念!!地球は無傷だ!!";
        }
         //再生したいclipを指定して
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
