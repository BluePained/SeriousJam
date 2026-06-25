using UnityEngine;

/// <summary>
/// Manages the customer behaviour.
/// </summary>
internal sealed class Customer : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private FoodSO[] foods;
    [SerializeField] private Vector2 waitingTimeInterval = new Vector2(3f, 4f);
    [SerializeField] private float itemPickTime = 0.8f;
    [Header("Refrences")]
    [SerializeField] private MeshRenderer foodDialogue;
    [SerializeField] private Material UpArrowMat;
    [SerializeField] private Material DownArrowMat;

    internal CustomerQueue customerQueue;
    internal bool canBeUsed = false;
    private float waitingTime = default;
    private FoodSO chosedFood;

    private void OnEnable()
    {
        if (!this.canBeUsed) return;
        this.waitingTime = Random.Range(waitingTimeInterval.x, waitingTimeInterval.y);
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
}
