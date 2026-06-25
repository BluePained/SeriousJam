using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrillManager : InteractableObject
{
    [SerializeField] private GrillSlot[] placeObjectPoint;
    [Range(0,5)] [SerializeField] private float heatLevel;
    [SerializeField] private float heatLossDuration;
    [SerializeField] private float heatLossRate = 1;
    public float HeatLevel => heatLevel;
    private FoodBase _food;
    private float _heatCooldown;
    private float _heatCooldownRate;

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
        if (!PlayerManager.Instance.GetPlayerHandState()) return;
        
        foreach (GrillSlot slot in placeObjectPoint)
        {
            if (slot.IsUsed) continue;

            _food = PlayerManager.Instance.GetFoodFromPlayer();
            
            if (_food != null)
            {
                slot.ChangeUsedState(true);
                slot.AssignFood(_food);
                PlayerManager.Instance.ClearFoodFromPlayer();
                
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
    

    public void HeatUp(float heatAmplify)
    {
        float rounded = Mathf.Floor(heatAmplify * 100)/100;
        heatLevel += rounded;
        if (heatLevel > 5)
        {
            heatLevel = 5;
        }
        
        ResetCooldownTimer();
    }

    private void ResetCooldownTimer()
    {
        _heatCooldown = heatLossDuration;
    }
    
    private void Update()
    {
        _heatCooldown -= Time.deltaTime;
        
        if (_heatCooldown <= 0 && heatLevel > 0)
        {
            _heatCooldownRate += Time.deltaTime;

            if (_heatCooldownRate >= heatLossRate)
            {
                float heatLevelLost = Random.Range(0.1f, 0.3f);
                float rounded = Mathf.Floor(heatLevelLost * 100) / 100;
                heatLevel -= rounded;
                _heatCooldownRate = 0;
                
                if (heatLevel < 0)
                    heatLevel = 0;
            }
            
        }
        
        if(heatLevel <= 0) return;
        
        foreach (var grillSlot in placeObjectPoint)
        { 
            if (grillSlot.IsUsed)
            { 
                grillSlot.CookTheFood(heatLevel);
            }
        }
        
    }
}
