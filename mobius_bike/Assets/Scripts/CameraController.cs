using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraType
{
    MainCamera,
    UICamera,
    Selfie,
    BirdView,
    FirstPerson,
    Menu
}

public class CameraController : MonoBehaviour
{
    [SerializeField] private CameraType _cameraType;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        CameraSwitcher.Instance.RegisterCamera(_cameraType, this);
    }

    private void OnDestroy()
    {
        CameraSwitcher.Instance.UnregisterCamera(_cameraType);
    }

    public void Enable() => _camera.enabled = true;
    public void Disable() => _camera.enabled = false;
}
