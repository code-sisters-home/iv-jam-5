using CodeSisters.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraSwitcher : Singleton<CameraSwitcher>
{
    public bool IsReady;

    private Dictionary<CameraType, CameraController> _cameras = new Dictionary<CameraType, CameraController>();

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => AllCamerasLoaded());

        IsReady = true;
    }

    private bool AllCamerasLoaded()
    {
        return _cameras.Values.Count > 0 && _cameras.Values.All(_ => _ != null);
    }

    public void RegisterCamera(CameraType cameraType, CameraController cameraController)
    {
        if (!_cameras.TryAdd(cameraType, cameraController))
            UnityLogger.LogError($"Camera of type {cameraType} exists. Can't add {cameraController.name}");
    }
    
    public void UnregisterCamera(CameraType cameraType)
    {
        if (_cameras.TryGetValue(cameraType, out var camera))
        {
            _cameras.Remove(cameraType);
            Destroy(camera.gameObject);
        }
    }

    public void SwitchCamera(CameraType targetCameraType)
    {
        if (!IsReady) return;
        foreach (var camKV in _cameras)
        {
            if (camKV.Key == targetCameraType)
                camKV.Value.Enable();
            else
                camKV.Value.Disable();
        }
    }
}
