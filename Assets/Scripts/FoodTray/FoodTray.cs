using UnityEngine;

public class FoodTray : InteractableObject
{
    [SerializeField] private GameObject foodPrefab;

    public override void Interact(RaycastHit hit)
    {
        if (PlayerManager.Instance.GetPlayerHandState() || foodPrefab == null) return;
        globalAudio_SFX.instance.Play("putCookedFood");
        GameObject obj = Instantiate(foodPrefab);
        PlayerManager.Instance.AssignFoodToPlayer(obj.GetComponent<FoodBase>());
    }
}
