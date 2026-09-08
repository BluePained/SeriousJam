using Input;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomCinemachine
{
    public class CinemachineAxisController : MonoBehaviour
    {
        [SerializeField] private CinemachineInputAxisController inputAxisController;
        [SerializeField] private InputActionReference lookAction;
        private void OnEnable()
        {
            InputManager.OnActionMapChanged += OnActionMapChanged;
            //GlobalSettings.Instance.OnSensitivityChanged += OnSensitivityChanged;
            GlobalSettings.Instance.OnSensitivityChanged += ApplySensitivity;
            ApplySensitivity(GlobalSettings.Instance.CamSensitivity);
        }

        private void OnDisable()
        {
            InputManager.OnActionMapChanged -= OnActionMapChanged;
            //GlobalSettings.Instance.OnSensitivityChanged -= OnSensitivityChanged;
            GlobalSettings.Instance.OnSensitivityChanged -= ApplySensitivity;
        }



        private void ApplySensitivity(int NewSens)
        {
            lookAction.action.ApplyParameterOverride("scaleVector2:x", NewSens);
            lookAction.action.ApplyParameterOverride("scaleVector2:y", NewSens);
        }

        // Sensitivity through Gain
        /*
        private void OnSensitivityChanged(int NewSens)
        {
            if (inputAxisController == null) return;
            {
                // X Axis Sens
                SetNewSens("Look X (Pan)", NewSens);
                // Y Axis Sens
                SetNewSens("Look Y (Tilt)", -NewSens);
            }
        }
        */

        private void OnActionMapChanged(InputActionMap actionMap)
        {
            if (actionMap == (InputActionMap)InputManager.InputAction.Player)
            {
                EnableLook();
            }
            else
            {
                DisableLook();
            }
        }

        private void EnableLook()
        {
            SetAxisEnabled("Look X (Pan)", true);
            SetAxisEnabled("Look Y (Tilt)", true);
        }

        private void DisableLook()
        {
            SetAxisEnabled("Look X (Pan)", false);
            SetAxisEnabled("Look Y (Tilt)", false);
        }

        private void SetAxisEnabled(string axisName, bool enabledState)
        {
            if (inputAxisController != null)
            {
                foreach (var c in inputAxisController.Controllers)
                {
                    if (c.Name == axisName)
                    {
                        c.Enabled = enabledState;
                        return;
                    }
                }
            }
        }

        // Sensivity trough gain
        /*
        private void SetNewSens(string axisName, float NewSens) { 
            foreach (var c in inputAxisController.Controllers)
            {
                if (c.Name == axisName)
                {
                    c.Input.Gain = NewSens;
                }
            }
        }
        */
    }
}