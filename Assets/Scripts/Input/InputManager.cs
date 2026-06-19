using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        public static readonly InputSystem_Actions InputAction = new InputSystem_Actions();

        public static event Action<InputActionMap> OnActionMapChanged;

        public static void ToggleActionMap(InputActionMap actionMap)
        {
            if (actionMap.enabled) return;
        
            InputAction.Disable();
            OnActionMapChanged?.Invoke(actionMap);
            actionMap.Enable();
        }
    }
}
