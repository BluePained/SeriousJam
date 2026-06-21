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
    Burnt
}

public enum FoodState
{
    OnDrag,
    OnPlaced,
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
            >= 115 => Cookedness.Burnt,
            _ => cookedness
        };
        
        if(cookedness == Cookedness.Burnt) _isBurned = true;
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
    [SerializeField] private Vector3 pickUpRotation = new Vector3(60,0,0);
    
    private PlaceSlot _slot;
    
    private Camera _camera;
    
    private void Awake()
    {
        if (foodData == null) return;
        
        side = foodData.Side;
        currentSide = Side.Up;
        transform.localRotation = Quaternion.Euler(pickUpRotation);
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (foodState == FoodState.OnDrag)
        {
            Vector3 screenPos = Pointer.current.position.ReadValue();
            Vector3 screenPos3D = new Vector3(screenPos.x, screenPos.y, dragZOffset);
            Vector3 pos = _camera.ScreenToWorldPoint(screenPos3D);
        
            transform.position = pos;
        }
        
    }

    public void ChangeLayer(int index)
    {
        gameObject.layer = index;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = index;
        }
    }
    
    public void ChangeState(FoodState newState)
    {
        foodState = newState;
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

    public override void Interact()
    {
        switch (foodState)
        {
            case FoodState.OnCooking:
            case FoodState.OnPlaced:
                PickUp();
                break;
            default:
                break;
        }
    }

    private void PickUp()
    {
        GameManager.Instance.AssignFoodToPlayer(this.gameObject);
        transform.localRotation = Quaternion.Euler(pickUpRotation);
        _slot?.ChangeUsedState(false);
        _slot = null;
        foodState = FoodState.OnDrag;
        ChangeLayer(6);
    }

    public void AssignSlot(PlaceSlot slot)
    {
        _slot = slot;
    }
    
}
