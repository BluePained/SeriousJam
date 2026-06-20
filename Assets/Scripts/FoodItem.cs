using System;
using UnityEngine;

public enum Side
{
    Up,
    Left,
    Right,
    Down
}

public enum Cookedness
{
    Raw,
    Undercooked,
    Cooked,
    Overcooked,
    Burned
}

[Serializable]
public class FoodSide
{
    [SerializeField] private Side side;
    [SerializeField] private Cookedness cookedness;
    [Range(0,115)] [SerializeField] private float cookednessValue;
    private bool _isBurned;
    private const float DEFAULT_COOK_VALUE = 0;
    private const float MAX_COOK_VALUE = 115;
    
    public void ChangeSide(Side changeSide)
    {
        side = changeSide;
    }

    public void AddCookedValue(float value)
    {
        if (_isBurned) return;
        
        cookednessValue += value;
        
        if(cookednessValue > MAX_COOK_VALUE)
            cookednessValue = MAX_COOK_VALUE;
        
        cookedness = cookednessValue switch
        {
            <= 25 => Cookedness.Raw,
            <= 50 => Cookedness.Undercooked,
            <= 75 => Cookedness.Cooked,
            <= 100 => Cookedness.Overcooked,
            >= 115 => Cookedness.Burned,
            _ => cookedness
        };
        
        if(cookedness == Cookedness.Burned) _isBurned = true;
    }
    
    public void DefaultValue()
    {
        cookednessValue = DEFAULT_COOK_VALUE;
        cookedness = Cookedness.Raw;
        _isBurned = false;
    }
}

public class FoodItem : Item
{
    [SerializeField] private FoodSide[] side;
    [SerializeField] private FoodSO foodData;
    [SerializeField] private Side currentSide;
    private bool _isCooking;
    
    private void Awake()
    {
        if (foodData == null) return;
        
        side = foodData.Side;
    }

    private void Update()
    {
        if (foodData is null || !_isCooking) return;
    }
    
}
