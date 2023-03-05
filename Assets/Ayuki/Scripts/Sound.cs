using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager; 

    [SerializeField]
    private AudioClip clip1; //�����f�[�^1

    [SerializeField]
    private AudioClip clip2; //�����f�[�^2
    // Start is called before the first frame update
    void Start()
    {
        soundManager.Play(clip1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
