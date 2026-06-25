using UnityEngine;

public class FoodTray : InteractableObject
{
    [SerializeField] private GameObject foodPrefab;

    public override void Interact()
    {
        if (PlayerManager.Instance.GetPlayerHandState()) return;
        
        GameObject obj = Instantiate(foodPrefab);
        PlayerManager.Instance.AssignFoodToPlayer(obj.GetComponent<FoodBase>());
    }
}
