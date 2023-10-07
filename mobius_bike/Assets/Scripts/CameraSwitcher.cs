using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera FirstPerCamera;
    public Camera SelfieCamera;
    
    public void SwitchToFirstPerCamera()
    {
        SelfieCamera.enabled = false;
        FirstPerCamera.enabled = true;
    }

    public void SwitchToSelfieCamera()
    {
        SelfieCamera.enabled = true;
        FirstPerCamera.enabled = false;
    }
}
