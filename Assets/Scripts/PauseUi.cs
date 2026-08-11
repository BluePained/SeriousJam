using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseUi : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private Slider SensitivitySlider;

    private bool Paused = false;
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        SensitivitySlider.value = GlobalSettings.Instance.CamSensitivity;
    }

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        inputActions.UI.Pause.performed += OnPausePressed;
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= OnPausePressed;
        inputActions.Disable();
    }

    private void OnPausePressed(InputAction.CallbackContext context)
    {
        if (PauseMenu == null)
        {
            return;
        }
        print("PAUSED");
        Paused = !Paused;
        PauseMenu.SetActive(Paused);
        if (Paused)
        {
            CursorManager.Instance.ChangeCursorState(CursorLockMode.None);
            Time.timeScale = 0.0f;
        }
        else
        {
            CursorManager.Instance.ChangeCursorState(CursorLockMode.Locked);
            Time.timeScale = 1.0f;
        }
    }

    public void OnSensitivitySliderValueChanged()
    {
        GlobalSettings.Instance.CamSensitivity = (int)SensitivitySlider.value;
        SensitivitySlider.TryGetComponent<TextMeshProUGUI>(out var textMeshPro);
        if (textMeshPro == null) return;
        textMeshPro.text = SensitivitySlider.value.ToString();
    }

}
