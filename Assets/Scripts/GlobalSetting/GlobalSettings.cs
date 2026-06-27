using System;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance;

    [Header("Sounds Settings")]
    [field: SerializeField]
    public float MainVolume { get; private set; }
    [field: SerializeField] public float MusicVolume  { get; private set; }
    [field: SerializeField] public float SfxVolume { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
