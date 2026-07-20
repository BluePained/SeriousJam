using UnityEngine;

public class MainMenuStartButton : InteractableObject
{
    [SerializeField] private string PlayScene;
    public override void Interact(RaycastHit hit)
    {
        CursorManager.Instance.ChangeCursorState(CursorLockMode.Locked);
        SceneLoaderManager.Instance.SceneLoad(PlayScene);
    }
}
