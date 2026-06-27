using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    
    [SerializeField] private Image cursor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void ChangeCursorState(CursorLockMode mode)
    {
        print("Changing cursor state: " + mode);
        Cursor.lockState = mode;

        if (mode == CursorLockMode.Locked)
        {
            cursor.rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            print("Move cursor");
            cursor.rectTransform.position = Pointer.current.position.ReadValue();
        }
    }
}
