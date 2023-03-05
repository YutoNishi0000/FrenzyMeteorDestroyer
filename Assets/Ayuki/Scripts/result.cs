using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class result : MonoBehaviour
{
    [SerializeField]
    private AudioSource source; //�X�s�[�J�[�ECD�v���C���[

    [SerializeField]
    private AudioClip clip1; //�����f�[�^1

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
                gameObject.GetComponent<Text>().text = "���߂łƂ�!!�n���͏����Ă��܂�����!!";
                earth.SetActive(false);
                source.clip = clip1;
            }
            else if(MeteoriteController.lustHP == 2)
            {
                gameObject.GetComponent<Text>().text = "�i�C�X!!�n���͂قډ�ł�����!!";
                earth.GetComponent<SpriteRenderer>().sprite = spriteearth2;
                source.clip = clip2;
            }
            else if (MeteoriteController.lustHP == 1)
            {
                gameObject.GetComponent<Text>().text = "�������!!�����͐�ł�����!!";
                earth.GetComponent<SpriteRenderer>().sprite = spriteearth3;
                source.clip = clip2;
            }
                
        }
        else{
            source.clip = clip2;
            gameObject.GetComponent<Text>().text ="�c�O!!�n���͖�����!!";
        }
         //�Đ�������clip���w�肵��
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
