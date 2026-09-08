using System;
using UnityEngine;
using UnityEngine.Audio;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance { get; private set; }

   // public bool GamePaused = false;

    public Action<int> OnSensitivityChanged;

    [Header("Sounds Settings")]

    [SerializeField]
    private AudioMixer _audioMixer;
    
    [SerializeField]
    private float _musicVolume = 0f;
    public float MusicVolume
    {
        get { return _musicVolume; }
        set {
            ChangeMixerVol("MusicVol", value);
            _musicVolume = value; 
        }
    }

    [SerializeField]
    private float _sfxVolume = 0f;
    public float SfxVolume
    {
        get { return _sfxVolume; }
        set {
            ChangeMixerVol("SfxVol", value);
            _sfxVolume = value; 
        }
    }

    [SerializeField]
    private float _mainVolume = 0f;
    public float MainVolume
    {
        get { return _mainVolume; }
        set {
            ChangeMixerVol("MasterVol", value);
            _mainVolume = value; 
        }
    }

    [SerializeField]
    private int _camSensitivity = 10;
    // Imm not invoking the onSensiChanged event on start.. Butttttt 
    // The pause menu, on load pulls this CamSensitivity property (returning the default one we set above) and changes the sensitivity slider value to whatever it is,
    // which invokes the sensi changed event there, which changes this property while also invoking the on Sensi changed event here
    // which ACTUALLY sets the default _camSensitivity
    public int CamSensitivity
    {
        get { return _camSensitivity; }
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

    private void ChangeMixerVol(string Parameter, float Volume)
    {
        if (_audioMixer == null) { return; }

        float ClampedVolume = Mathf.Clamp(Volume, 0.1f, 100f);
        float Db = 20f * Mathf.Log10(ClampedVolume * 0.01f);
        
        _audioMixer.SetFloat(Parameter, Db);
    }
}
