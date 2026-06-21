using UnityEngine;
using TMPro;

/// <summary>
/// Manages the queue of customers in the game.
/// </summary>
internal sealed class CustomerQueue : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private uint queue = 10;
    [SerializeField] private Vector2 timeBetweenCustomers = new Vector2(0.4f, 1.3f); // x: min; y: max
    [Header("Refrences")]
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private TextMeshProUGUI queueHudText;

    private GameObject currentCustomer;

    private void Start()
    {
        this.currentCustomer = Instantiate(customerPrefab);
        this.currentCustomer.transform.position = this.customerSpawnPoint.position;
        this.currentCustomer.SetActive(false);
        this.currentCustomer.GetComponent<Customer>().customerQueue = this;
        this.currentCustomer.GetComponent<Customer>().canBeUsed = true;
        queueHudText.text = $"Queue: {this.queue}";
        Invoke(nameof(SpawnCustomer), 3f);
    }
    internal void ManageQueue()
    {
        if (this.queue > 0)
            SpawnCustomer();
    }
    private void SpawnCustomer()
    {
        this.currentCustomer.SetActive(true);
    }
    internal void RemoveCustomer()
    {
        this.currentCustomer.SetActive(false);
        this.queue--;
        Invoke(nameof(ManageQueue), Random.Range(this.timeBetweenCustomers.x, this.timeBetweenCustomers.y));
        this.queueHudText.text = $"Queue: {this.queue}";
        // Debug.Log($"Customer Queue: {this.queue}");
    }
}
