using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class FoodBase : InteractableObject, IFlippable
{
    [SerializeField] protected FoodSO foodData;
    [SerializeField] protected FoodState foodState;
    [SerializeField] protected Side currentSide;
    [SerializeField] protected float cookTime;
    [SerializeField] protected float dragZOffset = 1f;
    [SerializeField] protected Vector3 pickUpRotation = new Vector3(60,0,0);
    public FoodSO FoodData { get => foodData; set => foodData = value; }
    public Side CurrentSide => currentSide;
    protected PlaceSlot Slot;
    protected Camera MainCamera;
    protected float Timer;
    public event Action<Side> NotifyFlipping;
    public event Action<Side,Cookedness> NotifyCookedChanged;
    
    private void Update()
    {
        if (foodState == FoodState.OnDrag)
        {
            Vector3 screenPos = Pointer.current.position.ReadValue();
            screenPos.z = dragZOffset;
            Vector3 pos = MainCamera.ScreenToWorldPoint(screenPos);

            transform.position = pos;
        }
        
    }

    public void ChangeCookTime(float time)
    {
        cookTime = time;
    }
    
    public void ChangeLayer(int index)
    {
        gameObject.layer = index;

        void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
        
        SetLayerRecursively(gameObject, index);
    }


    public void ChangeState(FoodState newState)
    {
        foodState = newState;
    }
    

    protected void FlipOver()
    {
        currentSide = currentSide switch
        {
            Side.Front => Side.Back,
            Side.Back => Side.Front,
            Side.Left => Side.Right,
            Side.Right => Side.Left,
            _ => currentSide
        };
    }
    
    public override void Interact(RaycastHit hit)
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
        if (PlayerManager.Instance.PlayerData.IsHandHolding) return;
        PlayerManager.Instance.AssignFoodToPlayer(this);
        transform.localRotation = Quaternion.Euler(pickUpRotation);
        globalAudio_SFX.instance.Play("grabbing");
        Slot?.ChangeUsedState(false);

        if (Slot is GrillSlot grillSlot)
        {
            grillSlot.RemoveFood();
        }
        
        Slot = null;
        foodState = FoodState.OnDrag;
        ChangeLayer(6);
    }
    
    public void AssignSlot(PlaceSlot slot)
    {
        Slot = slot;
    }

    protected virtual void FlipLeft()
    {
        
    }
    
    protected virtual void FlipRight()
    {
        
    }

    public virtual void Flip(string flipName)
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

    public virtual void CookFood(float heat)
    {
        
    }

    protected Side GetOppositeSide()
    {
        Side oppositeSide = currentSide switch
        {
            Side.Front => Side.Back,
            Side.Back => Side.Front,
            Side.Left => Side.Right,
            Side.Right => Side.Left,
            _ => currentSide
        };
        
        return oppositeSide;
    }

    public virtual void FoodCookednessSignalReceiver(Side side,Cookedness state)
    {
        NotifyCookedChanged?.Invoke(side,state);
    }
}
