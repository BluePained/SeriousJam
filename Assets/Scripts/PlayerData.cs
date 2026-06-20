using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerData : MonoBehaviour
{
    [field: SerializeField] public Transform HandPos { get; private set; }
    [field: SerializeField] public bool IsHandHolding { get; private set; }
    [field: SerializeField] public GameObject FoodObject { get; private set; }
    [SerializeField] private Camera mainCamera;
    private Vector2 _mousePos;
    RaycastHit _hit;

    private void OnEnable()
    {
        InputManager.InputAction.Player.Click.canceled += FiredRaycast;
    }

    private void OnDisable()
    {
        InputManager.InputAction.Player.Click.canceled -= FiredRaycast;
    }
    
    private void Start()
    {
        InputManager.ToggleActionMap(InputManager.InputAction.Player);    
        mainCamera = Camera.main;    
    }

    public bool AssignFoodToHand(GameObject food)
    {
        if(IsHandHolding) return false;
        
        IsHandHolding = true;
        FoodObject = food;
        return true;
    }

    private void FiredRaycast(InputAction.CallbackContext context)
    {
        print("Fired raycast");
        
        _mousePos = Pointer.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(_mousePos);

        if (!Physics.Raycast(ray, out _hit)) return;
        
        if (_hit.collider.TryGetComponent<InteractableObject>(out var interactable))
        {
            interactable?.Interact();
        }
    }

    public void ClearFoodFromPlayer()
    {
        IsHandHolding = false;
        FoodObject = null;
    }
}
