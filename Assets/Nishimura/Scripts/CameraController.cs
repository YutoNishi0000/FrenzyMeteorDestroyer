using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラを隕石に追従させる
public class CameraController : Actor
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Instance.transform.position;
    }
}