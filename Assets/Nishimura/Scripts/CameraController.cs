using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�J������覐΂ɒǏ]������
public class CameraController : Actor
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Instance.transform.position;
    }
}