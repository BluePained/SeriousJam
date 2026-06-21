using UnityEngine;

/// <summary>
/// Manages the customer behaviour.
/// </summary>
internal sealed class Customer : MonoBehaviour
{
    [SerializeField] private Vector2 waitingTimeInterval = new Vector2(3f, 4f);

    internal CustomerQueue customerQueue;
    internal bool canBeUsed = false;
    private float waitingTime = default;

    private void OnEnable()
    {
        if (!this.canBeUsed) return;
        this.waitingTime = Random.Range(waitingTimeInterval.x, waitingTimeInterval.y);
        Invoke(nameof(WaitingTimeEnded), waitingTime);
    }
    private void FoodServed()
    {
        customerQueue.RemoveCustomer();
    }
    private void WaitingTimeEnded()
    {
        customerQueue.RemoveCustomer();
        Debug.Log("Customer angy and left noob");
    }
}
