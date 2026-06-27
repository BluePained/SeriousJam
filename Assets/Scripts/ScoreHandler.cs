using System;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text queueText;
    
    private void Start()
    {
        GameManager.Instance.OnScoreChange += UpdateScoreText;
        GameManager.Instance.OnQueueChange += UpdateQueueText;
        
        scoreText.text = "0";
        queueText.text = "0";

        scoreText.text = GameManager.Instance.ScoreManager.Score.ToString();
        queueText.text = GameManager.Instance.CustomerManager.CurrentCustomerQueue.ToString();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnScoreChange -= UpdateScoreText;
        GameManager.Instance.OnQueueChange -= UpdateQueueText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    private void UpdateQueueText(int queue)
    {
        queueText.text = queue.ToString();
    }
}
