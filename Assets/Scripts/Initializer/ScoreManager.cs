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
    }
    
    public void AddScore(int score)
    {
        Score += score;
    }
    
}
