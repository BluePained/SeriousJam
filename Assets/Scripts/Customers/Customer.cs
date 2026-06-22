using UnityEngine;

/// <summary>
/// Manages the customer behaviour.
/// </summary>
internal sealed class Customer : MonoBehaviour
{
    [SerializeField] private FoodSO[] foods;
    [SerializeField] private Vector2 waitingTimeInterval = new Vector2(3f, 4f);

    internal CustomerQueue customerQueue;
    internal bool canBeUsed = false;
    private float waitingTime = default;
    private FoodSO chosedFood;

    private void OnEnable()
    {
        if (!this.canBeUsed) return;
        this.waitingTime = Random.Range(waitingTimeInterval.x, waitingTimeInterval.y);
        ChooseFood();
        Invoke(nameof(WaitingTimeEnded), waitingTime);
    }
    private void FoodServed()
    {
        customerQueue.RemoveCustomer(this.gameObject);
    }
    private void WaitingTimeEnded()
    {
        customerQueue.RemoveCustomer(this.gameObject);
        Debug.Log("Customer angy and left noob");
    }
    private void ChooseFood()
    {
        chosedFood = foods[Random.Range(0, foods.Length)];
        Debug.Log($"Customer wants {chosedFood.FoodName}");
    }
}
