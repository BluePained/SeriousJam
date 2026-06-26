using System;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform[] customerSpawnPoints;
    [SerializeField] private int initialCustomerQueue;
    [SerializeField] private float customerQueueInterval;
    [SerializeField] private float customerWaitingTime;
    [SerializeField] private float customerMinWaitingTime;
    [SerializeField] private Customer[] customers;
    private int _currentCustomerQueue;
    public int CurrentCustomerQueue => _currentCustomerQueue;
    
    private void Awake()
    {
        if (customerSpawnPoints.Length > 0)
        {
            customers = new Customer[customerSpawnPoints.Length];

            for (int i = 0; i < customerSpawnPoints.Length; i++)
            {
                GameObject customerInstance = Instantiate(customerPrefab, customerSpawnPoints[i].position, Quaternion.identity);
                //customerInstance.gameObject.SetActive(false);
                customers[i] = customerInstance.GetComponent<Customer>();
            }
        }
    }
}
