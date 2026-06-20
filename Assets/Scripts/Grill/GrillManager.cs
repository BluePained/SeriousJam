using System;
using UnityEngine;

public class GrillManager : InteractableObject
{
    [SerializeField] private GrillSlot[] placeObjectPoint;
    private GameObject _food;

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
                
                FoodInteractableObject food = _food.GetComponent<FoodInteractableObject>();
                food.ChangeState(FoodState.OnCooking);
                food.ChangeLayer(LayerMask.NameToLayer("Default"));

                foreach (Transform child in food.transform)
                {
                    child.gameObject.layer = LayerMask.NameToLayer("Default");
                }
                
                food.transform.position = slot.transform.position;
                food.transform.rotation = slot.transform.parent.localRotation;
                break;
            }
        }
        
        
    }
    
}
