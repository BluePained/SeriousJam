using UnityEngine;

public class TrashManager : InteractableObject
{
    public override void Interact()
    {
        if (!GameManager.Instance.GetPlayerHandState()) return;

        GameObject obj = GameManager.Instance.GetFoodFromPlayer().gameObject;
        Destroy(obj);
        GameManager.Instance.ClearFoodFromPlayer();
        
    }
}
