using UnityEngine;
using TMPro;

/// <summary>
/// Manages the queue of customers in the game.
/// Setup:
///     place four empty gameobject where you want customers to spawn,
///     provide a customer prefab,
///     provide a refrence to queue text on ui
/// </summary>
internal sealed class CustomerQueue : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private uint queue = 12;
    [SerializeField] private Vector2 timeBetweenCustomers = new Vector2(0.4f, 1.3f); // x: min; y: max
    [Header("Refrences")]
    [SerializeField] private Transform[] customerSpawnPoints = new Transform[4];
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private TextMeshProUGUI queueHudText;

    private GameObject[] customers = new GameObject[4];

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            customers[i] = Instantiate(this.customerPrefab);
            customers[i].transform.position = customerSpawnPoints[i].position;
            customers[i].SetActive(false);
            Customer customer = customers[i].GetComponent<Customer>();
            customer.customerQueue = this;
            customer.canBeUsed = true;
        }
        queueHudText.text = $"Queue: {this.queue + GetActiveCustomerCount()}";
        for (int i = 0; i < 4; i++)
            Invoke(nameof(ManageQueue), Random.Range(0f, 2f));
    }
    internal void ManageQueue()
    {
        if (this.queue == 0)
        {
            if (this.GetActiveCustomerCount() == 0)
                PlayerManager.Instance.EndGame();
            return;
        }
        SpawnCustomer();
        queueHudText.text = $"Queue: {this.queue + GetActiveCustomerCount()}";
    }
    private void SpawnCustomer()
    {
        for (int i = 0; i < customers.Length; i++)
        {
            if (!customers[i].activeSelf)
            {
                customers[i].SetActive(true);
                this.queue--;
                this.queueHudText.text = $"Queue: {this.queue}";
                return;
            }
        }
    }
    internal void RemoveCustomer(GameObject customer)
    {
        customer.SetActive(false);
        this.queueHudText.text = $"Queue: {this.queue + GetActiveCustomerCount()}";
        Invoke(nameof(ManageQueue), Random.Range(this.timeBetweenCustomers.x, this.timeBetweenCustomers.y));
        // Debug.Log($"Customer Queue: {this.queue}");
    }
    private uint GetActiveCustomerCount()
    {
        uint count = 0;
        for (int i = 0; i < customers.Length; i++)
        {
            if (customers[i].activeSelf)
                count++;
        }
        return count;
    }
}