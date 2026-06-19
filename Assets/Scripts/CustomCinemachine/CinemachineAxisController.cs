using Input;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CustomCinemachine
{
    public class CinemachineAxisController : MonoBehaviour
    {
        [SerializeField] private CinemachineInputAxisController inputAxisController;

        private void OnEnable()
        {
            InputManager.OnActionMapChanged += OnActionMapChanged;
        }

        private void OnDisable()
        {
            InputManager.OnActionMapChanged -= OnActionMapChanged;
        }

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
    }
}