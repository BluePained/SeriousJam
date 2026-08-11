using System;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance { get; private set; }

    public Action<int> OnSensitivityChanged;



    [Header("Sounds Settings")]
    [field: SerializeField]
    public float MainVolume { get; private set; }
    [field: SerializeField] public float MusicVolume { get; private set; }
    [field: SerializeField] public float SfxVolume { get; private set; }


    [SerializeField]
    private int _camSensitivity = 10;
    // Imm not calling the onSensiChanged func on start.. Butttttt 
    // The pause menu, on load pulls this CamSensitivity property (returning the default one we set above) and changes the sensitivity slider value to whatever it is,
    // which invokes the sensi changed event there, which changes this property while also invoking the on Sensi changed event here
    // which ACTUALLY sets the default _camSensitivity
    public int CamSensitivity
    {
        get
        {
            return _camSensitivity;
        }
        set
        {
            _camSensitivity = Math.Clamp(value, 1, 20);
            OnSensitivityChanged?.Invoke(_camSensitivity);
        }
    }


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
