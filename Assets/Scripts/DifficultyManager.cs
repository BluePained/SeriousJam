using UnityEngine;

internal sealed class DifficultyManager : MonoBehaviour
{
    internal static DifficultyManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this.gameObject);
    }
}
