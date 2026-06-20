using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [field: SerializeField] public PlayerData PlayerData { get; private set; }

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
    }

    public bool GetPlayerHandState()
    {
        return PlayerData.IsHandHolding;
    }

    public bool AssignFoodToPlayer(GameObject food)
    {
        return PlayerData.AssignFoodToHand(food);
    }

    public GameObject GetFoodFromPlayer()
    {
        return PlayerData.FoodObject == null ? null : PlayerData.FoodObject;
    }

    public void ClearFoodFromPlayer()
    {
        PlayerData.ClearFoodFromPlayer();
    }
    
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
