using System;
using UnityEngine;

public class GrillManager : InteractableObject
{
    [SerializeField] private GrillSlot[] placeObjectPoint;
    private FoodInteractableObject _food;

#if UNITY_EDITOR
    
    private void OnValidate()
    {
        if(Application.isPlaying) return;
        if(transform.childCount - 1 <= 0) return;
        
        placeObjectPoint = new GrillSlot[transform.childCount - 1];

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            placeObjectPoint[i] = transform.GetChild(i + 1).GetComponent<GrillSlot>();
        }
        
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    public override void Interact()
    {
        if (!GameManager.Instance.GetPlayerHandState()) return;
        
        foreach (GrillSlot slot in placeObjectPoint)
        {
            if (slot.IsUsed) continue;

            _food = GameManager.Instance.GetFoodFromPlayer();
            
            if (_food != null)
            {
                slot.ChangeUsedState(true);
                GameManager.Instance.ClearFoodFromPlayer();
                
                _food.ChangeState(FoodState.OnCooking);
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
