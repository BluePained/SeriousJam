using System;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class HandCrank : InteractableObject/*, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler*/
{
    [SerializeField] private float rotSpeed = 1000f;
    [SerializeField] private GrillManager grillManager;
    private Camera _mainCamera;
    private float _previousAngle;
    private float _accumulatedAngle;
    private float _spinSpeed;
    
    private bool _isCranking;
    private Vector2 _grabDirection;

    private void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;
    }
    
    public override void Interact(RaycastHit hit)
    {
        _isCranking = true;
        InputManager.ToggleActionMap(InputManager.InputAction.UI);

        _grabDirection = (hit.point - transform.position).normalized;

        _previousAngle = transform.eulerAngles.z;
    }

    /*public void OnBeginDrag(PointerEventData eventData)
    {
        print("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("OnDrag");
        float zDist = Vector3.Distance(_mainCamera.transform.position, this.transform.position);
        Vector3 screenPos = new Vector3(eventData.position.x, eventData.position.y, zDist);
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(screenPos);
        Vector2 dir = (mouseWorldPos - this.transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float delta = Mathf.DeltaAngle(currentAngle, angle);
        
        if (delta < 0)
        {
            Vector3 currentEuler = transform.localEulerAngles;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(new Vector3(currentEuler.x, currentEuler.y, angle)), rotSpeed * Time.deltaTime);

            float newAngle = transform.eulerAngles.z;
            float rotatedDelta = Mathf.DeltaAngle(_previousAngle, newAngle);

            _spinSpeed = rotatedDelta / Time.deltaTime; //<- Track spin speed go weeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
            _accumulatedAngle += rotatedDelta;

            if (_accumulatedAngle <= -360f)
            {
                _accumulatedAngle += 360f;
                OnFullRotation();
            }
        }
        _previousAngle = transform.eulerAngles.z;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        globalAudio_SFX.instance.Stop("crank");
        CursorManager.Instance.ChangeCursorState(CursorLockMode.Locked);
    }*/
    
    private void Update()
    {
        if (!_isCranking)
            return;
        CursorManager.Instance.ChangeCursor(CursorType.grab);
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StopCranking();
            return;
        }
        
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        Vector2 tangent = new Vector2(
            _grabDirection.y,
            -_grabDirection.x);

        float amount = Vector2.Dot(mouseDelta, tangent);

        if (Mathf.Abs(amount) < 0.01f)
            return;

        if (amount <= 0f)
            return;
        
        float rotation = amount * rotSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, -rotation);

        _grabDirection =
            (Quaternion.Euler(0, 0, -rotation) * _grabDirection).normalized;

        float newAngle = transform.eulerAngles.z;
        float rotatedDelta = Mathf.DeltaAngle(_previousAngle, newAngle);

        _spinSpeed = rotatedDelta / Time.deltaTime;
        _accumulatedAngle += rotatedDelta;

        if (_accumulatedAngle <= -360f)
        {
            _accumulatedAngle += 360f;
            OnFullRotation();
        }

        _previousAngle = newAngle;
    }
    
    private void StopCranking()
    {
        _isCranking = false;
        InputManager.ToggleActionMap(InputManager.InputAction.Player);
        CursorManager.Instance.ChangeCursor(CursorType.normal);
        globalAudio_SFX.instance.Stop("crank");
    }

    private void OnFullRotation()
    {
        grillManager.HeatUp(Random.Range(0.1f, 1f));
        globalAudio_SFX.instance.Play("crank");
    }
    

}
