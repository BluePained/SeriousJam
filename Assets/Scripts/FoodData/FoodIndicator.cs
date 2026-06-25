using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FoodIndicator : MonoBehaviour
{
    [SerializeField] private FoodStateColorSO foodStateColor;
    [SerializeField] private SpriteRenderer[] indicatorImage;
    [FormerlySerializedAs("foodData")] [SerializeField] private FoodBase foodBase;
    [SerializeField] private Transform anchor;
    [SerializeField] private float rotateSpeed;
    private Quaternion _targetRotation;
    private void Awake()
    {
        foreach (var i in indicatorImage)
        {
            i.color = foodStateColor.Raw;
        }
        
        _targetRotation = Quaternion.Euler(0, 0, 180);
    }

    private void Start()
    {
        foodBase.NotifyFlipping += NotifyFlipping;
        foodBase.NotifyCookedChanged += NotifyHandler;
    }

    private void OnDisable()
    {
        foodBase.NotifyFlipping -= NotifyFlipping;
        foodBase.NotifyCookedChanged -= NotifyHandler;
    }
    
    private void Update()
    {
        anchor.localRotation = Quaternion.Slerp(anchor.localRotation, _targetRotation, Time.deltaTime * rotateSpeed);
    }

    private void NotifyFlipping(Side side)
    {
        switch (side)
        {
            case Side.Front: //Go to the back
                _targetRotation = Quaternion.Euler(0, 0, 180);
                break;
            case Side.Back: //Go to the front
                _targetRotation = Quaternion.Euler(0, 0, 0);
                break;
            case Side.Left: //Go to the right
                _targetRotation = Quaternion.Euler(0, 0, -90);
                break;
            case Side.Right: //Go the to left
                _targetRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }

    private void NotifyHandler(Side side, Cookedness cookedness)
    {
        int sideIndex = (int)side;
        
        if (sideIndex > indicatorImage.Length)
            sideIndex = 1;
        
        print(sideIndex);
        
        indicatorImage[sideIndex].color = cookedness switch
        {
            Cookedness.Raw => foodStateColor.Raw,
            Cookedness.Undercooked => foodStateColor.Undercooked,
            Cookedness.Cooked => foodStateColor.Cooked,
            Cookedness.Burnt => foodStateColor.Burnt,
            _ => default
        };
    }

}
