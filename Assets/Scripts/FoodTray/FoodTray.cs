using UnityEngine;

public class FoodTray : InteractableObject
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private Vector3 offsetPosition;
    [SerializeField] private Vector3 offsetRotation;

    public override void Interact()
    {
        if (GameManager.Instance.GetPlayerHandState()) return;
        
        GameObject obj = Instantiate(foodPrefab, offsetPosition, Quaternion.Euler(offsetRotation));
        GameManager.Instance.AssignFoodToPlayer(obj);
    }
}
