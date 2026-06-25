using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodThreeState : FoodBase
{
    [SerializeField] protected FoodSideThreeState[] side;
    
    private Dictionary<Side, FoodSideThreeState> _sideMap;
    private void Awake()
    {
        if (foodData == null) return;
        
        cookTime = foodData.CookTime;
        
        FoodSOThreeState polyFoodData = (FoodSOThreeState)foodData;
        
        side = new FoodSideThreeState[polyFoodData.Side.Length];
        _sideMap = new Dictionary<Side, FoodSideThreeState>();
        for (int i = 0; i < side.Length; i++)
        {
            side[i] = new FoodSideThreeState(polyFoodData.Side[i].GetSide());
            _sideMap[side[i].GetSide()] = side[i];
        }
        currentSide = Side.Front;
        transform.localRotation = Quaternion.Euler(pickUpRotation);
    }
    
    private void Start()
    {
        MainCamera = Camera.main;

        foreach (var s in side)
        {
            s.OnCookednessChanged += FoodCookednessSignalReceiver;
        }
    }

    private void OnDisable()
    {
        foreach (var s in side)
        {
            s.OnCookednessChanged -= FoodCookednessSignalReceiver;
        }
    }
    
    
    protected override void FlipLeft()
    {
        if (side.Length == 2)
        {
            FlipOver();
            return;
        }

        currentSide = currentSide switch
        {
            Side.Front => Side.Right,
            Side.Right => Side.Back,
            Side.Back => Side.Left,
            Side.Left => Side.Front,
            _ => currentSide
        };
    }
    
    protected override void FlipRight()
    {
        if (side.Length == 2)
        {
            FlipOver();
            return;
        }
        
        currentSide = currentSide switch
        {
            Side.Front => Side.Left,
            Side.Left => Side.Back,
            Side.Back => Side.Right,
            Side.Right => Side.Front,
            _ => currentSide
        };
    }
    
    public override void CookFood(float heat)
    {
        Timer += Time.deltaTime;
        if (Timer >= cookTime)
        {
            if (_sideMap.TryGetValue(GetOppositeSide(), out FoodSideThreeState currentFoodSide))
                currentFoodSide.Cooking(heat);
            
            Timer = 0;
        }
    }
}
