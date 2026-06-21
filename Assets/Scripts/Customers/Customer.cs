using UnityEngine;

/// <summary>
/// Manages the customer behaviour.
/// </summary>
internal sealed class Customer : MonoBehaviour
{
    internal CustomerQueue customerQueue;

    private void Start()
    {
        Invoke(nameof(FoodServed), 1f);
    }
    private void FoodServed()
    {
        customerQueue.RemoveCustomer();
    }
}
