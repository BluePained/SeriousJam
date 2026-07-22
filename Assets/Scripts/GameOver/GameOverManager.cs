using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string MainMenuScene;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text consecutiveScoreText;
    [SerializeField] private TMP_Text perfectServedText;
    [SerializeField] private TMP_Text wrongOrderText;
    [SerializeField] private TMP_Text wrongCookednessServedText;
    [SerializeField] private TMP_Text unservedCustomerText;
    [SerializeField] private TMP_Text totalCustomerServedText;
    [SerializeField] private TMP_Text totalCustomerOrder;
    

    private void Start()
    {
        CursorManager.Instance.ChangeCursorState(CursorLockMode.Confined);
        var scoreManager = GameManager.Instance.ScoreManager;
        
        scoreText.text = $"Score: {scoreManager.Score}";
        consecutiveScoreText.text = $"Consecutive Perfect Served: {scoreManager.ConsecutivePerfectValue}";
        perfectServedText.text = $"Perfect Served: {scoreManager.PerfectServed}";
        wrongOrderText.text = $"Wrong Order Served: {scoreManager.WrongOrderServed}";
        wrongCookednessServedText.text = $"Wrong Cookedness Served: {scoreManager.WrongCookednessServed}";
        unservedCustomerText.text = $"Unserved Customer: {scoreManager.UnservedCustomer}";
        totalCustomerServedText.text = $"Total Customer Served: {scoreManager.TotalCustomerServed}";
        totalCustomerOrder.text = $"Total Customer Order: {scoreManager.TotalCustomerOrder}";
        
        Destroy(scoreManager);
    }
    
    public void LeaveGameOver()
    {
        Destroy(GameManager.Instance.gameObject);
        Destroy(CursorManager.Instance.gameObject);
        Destroy(globalAudio_SFX.instance.gameObject);
        Destroy(mainMusic.instance.gameObject);
        SceneLoaderManager.Instance.MoveToScene(MainMenuScene);
    }

}
