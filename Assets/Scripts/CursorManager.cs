using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum CursorType
{
    normal,
    click,
    trash,
    grab
}

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;
    
    [SerializeField] private Image cursor;
    [SerializeField] private Sprite normalCursor;
    [SerializeField] private Sprite clickableCursor;
    [SerializeField] private Sprite trashCursor;
    [SerializeField] private Sprite grabCursor;

    private readonly Vector2 _normalCursorSize = new Vector2(50,50);
    private readonly Vector2 _grabCursorSize = new Vector2(90,90);
    
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
        ChangeCursor(CursorType.normal);
    }

    public void ChangeCursor(CursorType cursorType)
    {
        if (!cursor) return;
       
        switch (cursorType)
        {
            case CursorType.normal:
                cursor.sprite = normalCursor;
                cursor.rectTransform.sizeDelta = _normalCursorSize;
                break;
            case CursorType.grab:
                cursor.sprite = grabCursor;
                cursor.rectTransform.sizeDelta = _grabCursorSize;
                break;
            case CursorType.trash:
                cursor.sprite = trashCursor;
                cursor.rectTransform.sizeDelta = _normalCursorSize;
                break;
            case CursorType.click:
                cursor.sprite = clickableCursor;
                cursor.rectTransform.sizeDelta = _normalCursorSize;
                break;
        }
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
            cursor.rectTransform.position = Pointer.current.position.ReadValue();
        }
    }
}
