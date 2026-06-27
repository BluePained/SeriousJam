using System;
using System.Collections.Generic;
using UnityEngine;

public class FoodFourState : FoodBase
{
    [SerializeField] protected FoodSideDefault[] side;
    public FoodSideDefault[] FoodSide => side;
    
    private Dictionary<Side, FoodSideDefault> _sideMap;
    private void Awake()
    {
        if (foodData == null) return;

        cookTime = foodData.CookTime;
        
        FoodSOFourState polyFoodData = (FoodSOFourState)foodData;
        
        side = new FoodSideDefault[polyFoodData.Side.Length];
        _sideMap = new Dictionary<Side, FoodSideDefault>();
        for (int i = 0; i < side.Length; i++)
        {
            side[i] = new FoodSideDefault(polyFoodData.Side[i].GetSide());
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
            if (_sideMap.TryGetValue(GetOppositeSide(), out FoodSideDefault currentFoodSide))
                currentFoodSide.Cooking(heat);
            
            Timer = 0;
        }
    }
    
    
}
