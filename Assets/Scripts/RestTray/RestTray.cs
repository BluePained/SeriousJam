using UnityEngine;

public class RestTray : InteractableObject
{
    [SerializeField] private RestTraySlot[] placeObjectPoint;
    private GameObject _food;
    
#if UNITY_EDITOR
    
    private void OnValidate()
    {
        if(Application.isPlaying) return;
        if(transform.childCount - 1 <= 0) return;
        
        placeObjectPoint = new RestTraySlot[transform.childCount - 1];

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            placeObjectPoint[i] = transform.GetChild(i + 1).GetComponent<RestTraySlot>();
        }
        
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
    
    public override void Interact()
    {
        if (!GameManager.Instance.GetPlayerHandState()) return;
        
        foreach (RestTraySlot slot in placeObjectPoint)
        {
            if (slot.IsUsed) continue;

            _food = GameManager.Instance.GetFoodFromPlayer();
            
            if (_food != null)
            {
                slot.ChangeUsedState(true);
                GameManager.Instance.ClearFoodFromPlayer();
                
                FoodInteractableObject food = _food.GetComponent<FoodInteractableObject>();
                food.ChangeState(FoodState.OnPlaced);
                food.ChangeLayer(LayerMask.NameToLayer("Default"));
                food.AssignSlot(slot);
                
                food.transform.position = slot.transform.position;
                food.transform.rotation = slot.transform.parent.localRotation;
                _food = null;
                break;
            }
        }
        
        
    }
}
