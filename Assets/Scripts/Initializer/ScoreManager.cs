using System;
using UnityEngine;

public enum ScoreType
{
    perfect,
    wrongOrder,
    wrongCookedness
}

public class ScoreManager : MonoBehaviour
{
    [field: SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int ConsecutivePerfectValue { get; private set; }
    
    public void ResetScore()
    {
        Score = 0;
    }

    public void DecreaseScore(int score)
    {
        ConsecutivePerfectValue = 0;
        Score -= score;
        
        if(Score < 0) Score = 0;
        
        GameManager.Instance.InvokeOnScoreChange(Score);
    }
    
    public void AddScore(int score, ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.perfect:
                ConsecutivePerfectValue++;
                
                if(ConsecutivePerfectValue % 3 == 0) GameManager.Instance.CustomerManager.AddCustomerQueue(ConsecutivePerfectValue - 1);
                
                break;
            case ScoreType.wrongCookedness:
                ConsecutivePerfectValue = 0;
                break;
        }
        
        Score += score;
        GameManager.Instance.InvokeOnScoreChange(Score);
    }
    
}
