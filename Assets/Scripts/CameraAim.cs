using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAim : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float zValue = 50;
    
    private Camera _camera;
    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        Vector3 mouseScreen = Pointer.current.position.ReadValue();
        mouseScreen.z = zValue;
        Vector3 worldPos = _camera.ScreenToWorldPoint(mouseScreen);
        float clampY = Mathf.Clamp(worldPos.y, minY, maxY);
        this.gameObject.transform.localPosition = new Vector3(0, clampY, zValue);
    }
}
