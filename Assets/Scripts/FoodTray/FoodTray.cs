using UnityEngine;

public class FoodTray : InteractableObject
{
    [SerializeField] private GameObject foodPrefab;

    public override void Interact()
    {
        if (GameManager.Instance.GetPlayerHandState()) return;
        
        GameObject obj = Instantiate(foodPrefab);
        GameManager.Instance.AssignFoodToPlayer(obj.GetComponent<FoodInteractableObject>());
    }
}
