using System;
using System.Collections;
using UnityEngine;

public class FoodVisualHandler : MonoBehaviour
{
    [SerializeField] private float flipSpeed = 10f;
    private MeshRenderer _meshRenderer;
    private FoodBase _foodData;
    private Quaternion _targetRotation = Quaternion.identity;
    private MaterialPropertyBlock _mpb;

    private void OnDisable()
    {
        _foodData.NotifyFlipping -= Flip;
        _foodData.NotifyCookedChanged -= HandleCookednessState;
    }

    private void Awake()
    {
        _foodData = GetComponentInParent<FoodBase>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _mpb = new MaterialPropertyBlock();
    }
    
    private void Start()
    {
        _foodData.NotifyFlipping += Flip;
        _foodData.NotifyCookedChanged += HandleCookednessState;
        _meshRenderer.SetPropertyBlock(_mpb);
    }
    

    private void Update()
    {
        if(_foodData is null) return;
        
        gameObject.transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, Time.deltaTime * flipSpeed);
    }

    private void Flip(Side flipSide)
    {
        switch (flipSide)
        {
            case Side.Left:
                _targetRotation = Quaternion.Euler(0, 0, -90);
                break;
            case Side.Right:
                _targetRotation = Quaternion.Euler(0, 0, 90);
                break;
            case Side.Front:
                _targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case Side.Back:
                _targetRotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }

    private void HandleCookednessState(Side side, Cookedness state)
    {
        _meshRenderer.GetPropertyBlock(_mpb);
        _mpb.SetFloat($"_State_{side}", (int)state);
        _meshRenderer.SetPropertyBlock(_mpb);
    }

    
}
