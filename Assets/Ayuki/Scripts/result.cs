using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result : MonoBehaviour
{
    [SerializeField]
    private AudioSource source; //スピーカー・CDプレイヤー

    [SerializeField]
    private AudioClip clip1; //音源データ1

    [SerializeField]
    private AudioClip clip2; 
    // Start is called before the first frame update
    void Start()
    {
        source.clip = clip1; //再生したいclipを指定して
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
