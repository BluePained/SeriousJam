using UnityEngine;

public class MainMenuStartButton : InteractableObject
{
    [SerializeField] private string PlayScene;
    public override void Interact()
    {
        SceneLoaderManager.Instance.SceneLoad(PlayScene);
    }
}
