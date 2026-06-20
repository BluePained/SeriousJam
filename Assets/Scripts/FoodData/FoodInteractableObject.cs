using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

public enum FoodState
{
    OnDrag,
    OnCooking
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

    public void Cooking(float value)
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

public class FoodInteractableObject : InteractableObject
{
    [SerializeField] private FoodState foodState;
    [SerializeField] private FoodSide[] side;
    [SerializeField] private FoodSO foodData;
    [SerializeField] private Side currentSide;
    [SerializeField] private float dragZOffset;
    
    private Camera _camera;
    
    private void Awake()
    {
        if (foodData == null) return;
        
        side = foodData.Side;
        currentSide = Side.Up;

        
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        switch (foodState)
        {
            case FoodState.OnDrag:
                Vector3 screenPos = Pointer.current.position.ReadValue();
                Vector3 screenPos3D = new Vector3(screenPos.x, screenPos.y, dragZOffset);
                Vector3 pos = _camera.ScreenToWorldPoint(screenPos3D);
        
                transform.position = pos;
                break;
            case FoodState.OnCooking:
                side[(int)currentSide].Cooking(1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    public void FlipLeft()
    {
        if (side.Length == 2)
        {
            FlipOver();
            return;
        }

        currentSide = currentSide switch
        {
            Side.Up => Side.Right,
            Side.Right => Side.Down,
            Side.Down => Side.Left,
            Side.Left => Side.Up,
            _ => currentSide
        };
    }

    public void FlipRight()
    {
        if (side.Length == 2)
        {
            FlipOver();
            return;
        }
        
        currentSide = currentSide switch
        {
            Side.Up => Side.Left,
            Side.Left => Side.Down,
            Side.Down => Side.Right,
            Side.Right => Side.Up,
            _ => currentSide
        };
    }

    public void FlipOver()
    {
        currentSide = currentSide switch
        {
            Side.Up => Side.Down,
            Side.Down => Side.Up,
            Side.Left => Side.Right,
            Side.Right => Side.Left,
            _ => currentSide
        };
    }
    
}
