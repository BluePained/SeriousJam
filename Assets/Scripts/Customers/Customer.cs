using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// Manages the customer behaviour.
/// </summary>
internal sealed class Customer : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private FoodSO[] foods;
    [SerializeField] private float waitingTimeInterval;
    [SerializeField] private float itemPickTime = 0.8f;
    [SerializeField] private float minWaitingTime;
    [Header("Refrences")]
    [SerializeField] private MeshRenderer foodDialogue;
    [SerializeField] private Material UpArrowMat;
    [SerializeField] private Material DownArrowMat;

    internal CustomerQueue customerQueue;
    internal float WaitingTimeInterval
    {
        get => waitingTimeInterval;
        set => waitingTimeInterval = Mathf.Max(this.minWaitingTime, value);
    }
    internal bool canBeUsed = false;
    private float waitingTime = default;
    private FoodSO chosedFood;

    private void OnEnable()
    {
        if (!this.canBeUsed) return;
        this.waitingTime = this.WaitingTimeInterval;
        Invoke(nameof(ChooseFood), itemPickTime);
        Invoke(nameof(WaitingTimeEnded), waitingTime + itemPickTime);
    }
    private void OnDisable()
    {
        foodDialogue.enabled = false;
        foodDialogue.material = null;
    }
    private void FoodServed()
    {
        customerQueue.RemoveCustomer(this.gameObject);
    }
    private void WaitingTimeEnded()
    {
        foodDialogue.material = DownArrowMat;
        Debug.Log("Customer angy and left noob");
        Invoke(nameof(LeaveShop), 1f);
    }
    private void ChooseFood()
    {
        foodDialogue.enabled = true;
        chosedFood = foods[Random.Range(0, foods.Length)];
        //foodDialogue.material = chosedFood.FoodSprite;
        Debug.Log($"Customer wants {chosedFood.FoodName}");
    }
    private void LeaveShop()
    {
        customerQueue.RemoveCustomer(this.gameObject);
    }
    private void Update()
    {
        Debug.Log(WaitingTimeInterval);
    }
}
