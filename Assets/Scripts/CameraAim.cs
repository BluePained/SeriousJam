using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAim : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float zValue = 50;

    private CinemachineCamera _cinemachineCamera;
    private Camera _camera;
    
    
    private void Start()
    {
        _camera = Camera.main;
        StartCoroutine(FindObject());
        GameManager.Instance.OnGameStateChange += NotifyHandler;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameStateChange -= NotifyHandler;
    }

    void Update()
    {
        if (GameManager.Instance.State == GameState.Playing || GameManager.Instance.State == GameState.None)
        {
            Vector3 mouseScreen = Pointer.current.position.ReadValue();
            mouseScreen.z = zValue;
            Vector3 worldPos = _camera.ScreenToWorldPoint(mouseScreen);
            float clampY = Mathf.Clamp(worldPos.y, minY, maxY);
            float clampX = Mathf.Clamp(worldPos.x, minX, maxX);
            this.gameObject.transform.localPosition = new Vector3(clampX, clampY, zValue);
        }
       
    }

    private IEnumerator FindObject()
    {
        while (_cinemachineCamera == null)
        {
            _cinemachineCamera = GameManager.Instance.CinemachineCamera;
            yield return null;
        }
        _cinemachineCamera.LookAt = this.gameObject.transform;
    }

    private void NotifyHandler(GameState state)
    {
        if (state == GameState.GameOver)
        {
            print("GameOver");
        }
    }
}
