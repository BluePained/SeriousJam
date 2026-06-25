using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private string[] loadingPlayScene;

    void Start()
    {
        SceneLoaderManager.Instance.AddSceneToLoad(loadingPlayScene);
    }

}
