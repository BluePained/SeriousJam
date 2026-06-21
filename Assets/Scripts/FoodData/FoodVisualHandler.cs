using System;
using System.Collections;
using UnityEngine;

public class FoodVisualHandler : MonoBehaviour
{
    [SerializeField] private float flipSpeed = 10f;
    private FoodInteractableObject _foodData;
    private Quaternion _targetRotation = Quaternion.identity;

    private void OnDisable()
    {
        _foodData.NotifyFlipping -= Flip;
    }
    
    private void Start()
    {
        _foodData = gameObject.transform.parent.GetComponent<FoodInteractableObject>();
        _foodData.NotifyFlipping += Flip;
    }

    private void Update()
    {
        if(_foodData is null) return;
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _targetRotation, Time.deltaTime * flipSpeed);
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
            case Side.Up:
                _targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case Side.Down:
                _targetRotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }

    
}
