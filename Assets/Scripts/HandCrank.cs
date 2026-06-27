using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class HandCrank : InteractableObject, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float rotSpeed = 1000f;
    [SerializeField] private GrillManager grillManager;
    private Camera _mainCamera;
    private float _previousAngle;
    private float _accumulatedAngle;
    private float _spinSpeed;

    private void Start()
    {
        _mainCamera = GameManager.Instance.MainCamera;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
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
    }

    private void OnFullRotation()
    {
        print("Full rotation");
        grillManager.HeatUp(Random.Range(0.1f, 1f));
        globalAudio_SFX.instance.Play("crank");
    }
}
