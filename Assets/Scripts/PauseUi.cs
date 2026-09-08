using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenu;

    private Dictionary<Slider, TextMeshProUGUI> SliderLabels = new();

    [SerializeField]
    private Slider SensitivitySlider;
    [SerializeField]
    private Slider MainVolSlider;
    [SerializeField]
    private Slider MusicVolSlider;
    [SerializeField]
    private Slider SFXVolSlider;

    [SerializeField]
    private bool Paused;
    private bool _paused
    {
        get { return Paused; }
        set
        {
            print("Paused: " + value);
            Paused = value;
            //GlobalSettings.Instance.GamePaused = Paused;
        }

    }


    private InputSystem_Actions inputActions;

    private void Awake()
    {
        PauseMenu.SetActive(false);
        SensitivitySlider.value = GlobalSettings.Instance.CamSensitivity;

    }

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.UI.Pause.performed += OnPauseShortCutPressed;

        MainVolSlider.value = GlobalSettings.Instance.MainVolume;
        MusicVolSlider.value = GlobalSettings.Instance.MusicVolume;
        SFXVolSlider.value = GlobalSettings.Instance.SfxVolume;
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= OnPauseShortCutPressed;
        inputActions.Disable();
    }

    private void OnPauseShortCutPressed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (PauseMenu == null || (GameManager.Instance.State != GameState.Playing && GameManager.Instance.State != GameState.Paused)) return;
        _paused = !_paused;
        PauseMenu.SetActive(_paused);
        if (_paused)
        {
            CursorManager.Instance.ChangeCursorState(CursorLockMode.None);
            CursorManager.Instance.ChangeCursor(CursorType.normal);
            Time.timeScale = 0.0f;
            GameManager.Instance.ChangeState(GameState.Paused);
        }
        else
        {
            CursorManager.Instance.ChangeCursorState(CursorLockMode.Locked);
            Time.timeScale = 1.0f;
            GameManager.Instance.ChangeState(GameState.Playing);
        }

    }


    public void OnSensitivitySliderValueChanged()
    {
        GlobalSettings.Instance.CamSensitivity = (int)SensitivitySlider.value;
        UpdateSliderLabel(SensitivitySlider);
    }

    public void OnMainVolSliderValueChanged()
    {
        GlobalSettings.Instance.MainVolume = MainVolSlider.value;
        UpdateSliderLabel(MainVolSlider);
    }

    public void OnMusicVolSliderValueChanged()
    {
        GlobalSettings.Instance.MusicVolume = MusicVolSlider.value;
        UpdateSliderLabel(MusicVolSlider);
    }

    public void OnSFXVolSliderValueChanged()
    {
        GlobalSettings.Instance.SfxVolume = SFXVolSlider.value;
        UpdateSliderLabel(SFXVolSlider);
    }

    private void UpdateSliderLabel(Slider TargetSlider)
    {
        if (!SliderLabels.ContainsKey(TargetSlider))
        {
            TargetSlider.TryGetComponent<TextMeshProUGUI>(out var textMeshPro);
            SliderLabels[TargetSlider] = textMeshPro;
        }
        if (SliderLabels[TargetSlider] == null) return;
        SliderLabels[TargetSlider].text = "<space=175px>" + Mathf.Floor(TargetSlider.value).ToString();
    }
}