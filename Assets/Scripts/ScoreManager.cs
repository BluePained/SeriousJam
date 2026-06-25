using TMPro;
using UnityEngine;

internal sealed class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyLabel;

    internal static ScoreManager Instance;
    private uint money = default;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this.gameObject);
    }
    internal void AddMoney(uint _money)
    {
        if (this.moneyLabel == null)
        {
            Debug.LogWarning("ScoreManager: money label not found.");
            return;
        }
        this.money += _money;
        this.moneyLabel.text = $"${this.money}";
    }
}
