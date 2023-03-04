using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ƒJƒƒ‰‚ğè¦Î‚É’Ç]‚³‚¹‚é
public class CameraController : Actor
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Instance.transform.position;
    }
}