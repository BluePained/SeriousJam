using System;
using UnityEngine;

public class CustomerMeter : MonoBehaviour
{
    [SerializeField] private GameObject customerMeter;

    public void Activate()
    {
        customerMeter.gameObject.SetActive(true);
    }

    public void Start()
    {
        customerMeter.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Reset();
    }

    public void Reset()
    {
        customerMeter.transform.localScale = new Vector3(3, 1, 1);
    }

    public void SetMeter(float ratio)
    {
        float targetX = 3f * ratio;
        customerMeter.transform.localScale = new Vector3(targetX, 1, 1);
    
        if (targetX <= 0) customerMeter.SetActive(false);
    }
}
