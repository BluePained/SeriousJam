using UnityEngine;

public class TrashManager : InteractableObject
{
    public override void Interact(RaycastHit hit)
    {
        if (!PlayerManager.Instance.GetPlayerHandState()) return;

        globalAudio_SFX.instance.Play("trash");
        GameObject obj = PlayerManager.Instance.GetFoodFromPlayer().gameObject;
        PlayerManager.Instance.ClearFoodFromPlayer();
        Destroy(obj);
        
    }
}
