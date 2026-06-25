using UnityEngine;

public class TrashManager : InteractableObject
{
    public override void Interact()
    {
        if (!PlayerManager.Instance.GetPlayerHandState()) return;

        GameObject obj = PlayerManager.Instance.GetFoodFromPlayer().gameObject;
        Destroy(obj);
        PlayerManager.Instance.ClearFoodFromPlayer();
        
    }
}
