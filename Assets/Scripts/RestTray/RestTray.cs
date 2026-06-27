using UnityEngine;

public class RestTray : InteractableObject
{
    [SerializeField] private RestTraySlot[] placeObjectPoint;
    private FoodBase _food;
    
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
    
    public override void Interact(RaycastHit hit)
    {
        if (!PlayerManager.Instance.GetPlayerHandState()) return;
        
        foreach (RestTraySlot slot in placeObjectPoint)
        {
            if (slot.IsUsed) continue;

            _food = PlayerManager.Instance.GetFoodFromPlayer();
            
            if (_food != null)
            {
                slot.ChangeUsedState(true);
                PlayerManager.Instance.ClearFoodFromPlayer();
                globalAudio_SFX.instance.Play("putCookedFood");
                _food.ChangeState(FoodState.OnPlaced);
                _food.ChangeLayer(LayerMask.NameToLayer("Default"));
                _food.AssignSlot(slot);
                
                _food.gameObject.transform.position = slot.transform.position;
                _food.gameObject.transform.rotation = slot.transform.parent.localRotation;
                _food = null;
                break;
            }
        }
        
        
    }
}
