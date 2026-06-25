using UnityEngine;

public class ButtonCaller : MonoBehaviour
{
    [SerializeField] private string[] loadingPlayScene;
    public void PlayGame()
    {
        SceneLoaderManager.Instance.AddSceneToLoad(loadingPlayScene);
    }
}
