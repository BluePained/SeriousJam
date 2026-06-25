using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Side
{
    Front,
    Left,
    Right,
    Back
}

public enum Cookedness
{
    Raw,
    Undercooked,
    Cooked,
    Burnt
}

public enum FoodState
{
    OnDrag,
    OnPlaced,
    OnCooking
}

[Serializable]
public abstract class FoodSide
{
    [SerializeField] protected Side side;
    [SerializeField] protected Cookedness cookedness;
    [Range(0,115)] [SerializeField] protected float cookednessValue;
    protected Cookedness CachedCookedness;
    protected const float DEFAULT_COOK_VALUE = 0;
    protected const float MAX_COOK_VALUE = 100;
    
    public event Action<Side,Cookedness> OnCookednessChanged;
    
    public Side GetSide()
    {
        return side;
    }
    
    public FoodSide(Side foodSide)
    {
        side = foodSide;
    }
    
    public void ChangeSide(Side changeSide)
    {
        side = changeSide;
    }
    
    public virtual void Cooking(float value)
    {

    }
    
    public void DefaultValue()
    {
        cookednessValue = DEFAULT_COOK_VALUE;
        cookedness = Cookedness.Raw;
    }

    public void OnCookednessChange()
    {
        OnCookednessChanged?.Invoke(side,cookedness);
    }
}

[Serializable]
public class FoodSideDefault : FoodSide
{
    public FoodSideDefault(Side foodSide) : base(foodSide)
    {
        side = foodSide;
    }

    public override void Cooking(float value)
    {
        if (cookedness == Cookedness.Burnt) return;
        
        cookednessValue += value;
        
        if(cookednessValue > MAX_COOK_VALUE)
            cookednessValue = MAX_COOK_VALUE;
        
        switch (cookednessValue)
        {
            case <= 25:
                cookedness = Cookedness.Raw;
                break;
            case <= 50:
                cookedness = Cookedness.Undercooked;
                break;
            case <= 75:
                cookedness = Cookedness.Cooked;
                break;
            case >= MAX_COOK_VALUE:
                cookedness = Cookedness.Burnt;
                break;
        }

        if (CachedCookedness != cookedness)
        {
            CachedCookedness = cookedness;
            OnCookednessChange();
        }

    }
    
}

[Serializable]
public class FoodSideThreeState : FoodSide
{
    public FoodSideThreeState(Side foodSide) : base(foodSide)
    {
        side = foodSide;
    }
    
    public override void Cooking(float value)
    {
        if (cookedness == Cookedness.Burnt) return;
        
        cookednessValue += value;
        
        if(cookednessValue > MAX_COOK_VALUE)
            cookednessValue = MAX_COOK_VALUE;
        
        switch (cookednessValue)
        {
            case <= 30:
                cookedness = Cookedness.Raw;
                break;
            case <= 65:
                cookedness = Cookedness.Cooked;
                break;
            case >= MAX_COOK_VALUE:
                cookedness = Cookedness.Burnt;
                break;
        }
        
        if (CachedCookedness != cookedness)
        {
            CachedCookedness = cookedness;
            OnCookednessChange();
        }

    }
}

public class FoodInteractableObject : MonoBehaviour
{
    /*[SerializeField] private FoodState foodState;
    [SerializeReference]
    [SerializeField] private FoodSide[] side;
    [SerializeField] private FoodSO foodData;
    [SerializeField] private Side currentSide;
    [SerializeField] private float dragZOffset;
    [SerializeField] private Vector3 pickUpRotation = new Vector3(60,0,0);
    
    public Side CurrentSide => currentSide;

    private Dictionary<Side, FoodSideDefault> _sideMap;
    private FoodVisualHandler _foodVisualHandler; 
    private PlaceSlot _slot;
    private Camera _camera;
    private float _timer;
    
    public event Action<Side> NotifyFlipping;
    */
 /*   private void Awake()
    {
        if (foodData == null) return;
        
        //side = foodData.Side;
        
        side = new FoodSideDefault[foodData.Side.Length];
        _sideMap = new Dictionary<Side, FoodSideDefault>();
        for (int i = 0; i < side.Length; i++)
        {
            side[i] = new FoodSideDefault(foodData.Side[i].GetSide());
            _sideMap[side[i].GetSide()] = side[i];
        }
        currentSide = Side.Up;
        transform.localRotation = Quaternion.Euler(pickUpRotation);
    }

    private void Start()
    {
        _foodVisualHandler = gameObject.transform.GetComponentInChildren<FoodVisualHandler>();
        _camera = Camera.main;
        
        if(_foodVisualHandler == null) print("Food Visual is null");
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
        if (GameManager.Instance.PlayerData.IsHandHolding) return;
        //GameManager.Instance.AssignFoodToPlayer(this);
        transform.localRotation = Quaternion.Euler(pickUpRotation);
        _slot?.ChangeUsedState(false);

        if (_slot is GrillSlot grillSlot)
        {
            grillSlot.RemoveFood();
        }
        
        _slot = null;
        foodState = FoodState.OnDrag;
        ChangeLayer(6);
    }

    public void AssignSlot(PlaceSlot slot)
    {
        _slot = slot;
    }

    public void Flip(string flipName)
    {
        switch (flipName)
        {
            case "FlipLeft":
                FlipLeft();
                break;
            case "FlipRight":
                FlipRight();
                break;
            case "FlipOver":
                FlipOver();
                break;
        }

        NotifyFlipping?.Invoke(currentSide);
    }

    public void CookFood(float heat)
    {
        _timer += Time.deltaTime;
        if (_timer >= foodData.CookTime)
        {
            if (_sideMap.TryGetValue(currentSide, out FoodSideDefault currentFoodSide))
                currentFoodSide.Cooking(heat);
            
            _timer = 0;
        }
    }*/
    
}
