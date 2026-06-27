using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [field: SerializeField] public int Score { get; private set; }
    
    public void ResetScore()
    {
        Score = 0;
    }

    public void DecreaseScore(int score)
    {
        Score -= score;
        
        if(Score < 0) Score = 0;
        
        GameManager.Instance.InvokeOnScoreChange(Score);
    }
    
    public void AddScore(int score)
    {
        Score += score;
        GameManager.Instance.InvokeOnScoreChange(Score);
    }
    
}
