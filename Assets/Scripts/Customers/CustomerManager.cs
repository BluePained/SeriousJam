using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] public FoodContainer foodContainer;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform[] customerSpawnPoints;
    [SerializeField] private int initialCustomerQueue;
    [SerializeField] private float customerQueueInterval;
    [SerializeField] private Vector2 customerIntervalTimer;
    [SerializeField] private float customerWaitingTime;
    [SerializeField] private float customerMinWaitingTime;
    [SerializeField] private Customer[] customers;
    [SerializeField] private int currentCustomerQueue;
    public int CurrentCustomerQueue => currentCustomerQueue;
    
    private void Awake()
    {
        if (customerSpawnPoints.Length > 0)
        {
            customers = new Customer[customerSpawnPoints.Length];

            for (int i = 0; i < customerSpawnPoints.Length; i++)
            {
                GameObject customerInstance = Instantiate(customerPrefab, customerSpawnPoints[i].position, Quaternion.identity);
                customerInstance.gameObject.SetActive(false);
                customers[i] = customerInstance.GetComponent<Customer>();
            }
        }
        
        currentCustomerQueue = initialCustomerQueue;
    }

    private void Start()
    {
        Invoke(nameof(StartQueue), 3f);
    }

    private void StartQueue()
    {
        GameManager.Instance.ChangeState(GameState.Playing);
    }
    public void UpdateCustomerWaitingTime()
    {
        
    }

    private int GetActivatedCustomer()
    {
        int cur = 0;
        foreach (Customer customer in customers)
        {
            if (customer.gameObject.activeSelf)
            {
                cur++;
            }
        }

        return cur;
    }
    
    private void Update()
    {
        switch (GameManager.Instance.State)
        {
            case GameState.Playing:
                if (currentCustomerQueue <= 0) return;
                if (GetActivatedCustomer() == 4)
                {
                    customerQueueInterval = 2;
                    return;
                }
                
                customerQueueInterval -= Time.deltaTime;
                
                if (customerQueueInterval <= 0)
                {
                    foreach (Customer customer in customers)
                    {
                        if (customer.gameObject.activeSelf) continue;

                        int foodIndex = Random.Range(0, foodContainer.FoodData.Length);
                        
                        customer.gameObject.SetActive(true);
                        customer.SetOrder(customerWaitingTime,foodContainer.FoodData[foodIndex]);
                        currentCustomerQueue--;
                        GameManager.Instance.InvokeOnQueueChange(currentCustomerQueue);
                        break;
                    }
                    
                    customerQueueInterval = Random.Range(customerIntervalTimer.x, customerIntervalTimer.y);
                }

                if (currentCustomerQueue == 0 && GetActivatedCustomer() == 0)
                {
                    GameManager.Instance.ChangeState(GameState.GameOver);
                }
                break;
        }

        
    }
}
