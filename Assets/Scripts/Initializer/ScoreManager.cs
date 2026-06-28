using System;
using UnityEngine;

public enum ScoreType
{
    perfect,
    wrongOrder,
    wrongCookedness,
    unserved
}

public class ScoreManager : MonoBehaviour
{
    [field: SerializeField] public int Score { get; private set; }
    [field: SerializeField] public int ConsecutivePerfectValue { get; private set; }
    [field: SerializeField] public int PerfectServed { get; private set; }
    [field: SerializeField] public int WrongOrderServed { get; private set; }
    [field: SerializeField] public int WrongCookednessServed { get; private set; }
    [field: SerializeField] public int UnservedCustomer { get; private set; }
    [field: SerializeField] public int TotalCustomerServed { get; private set; }
    [field: SerializeField] public int TotalCustomerOrder { get; private set; }
    
    private int _consecutiveBadValue;
    public void ResetScore()
    {
        Score = 0;
        ConsecutivePerfectValue = 0;
        PerfectServed = 0;
        WrongOrderServed = 0;
        WrongCookednessServed = 0;
        UnservedCustomer = 0;
        TotalCustomerServed = 0;
        TotalCustomerOrder = 0;
        
    }

    public void DecreaseScore(int score, ScoreType scoreType)
    {
        ConsecutivePerfectValue = 0;
        _consecutiveBadValue++;
        Score -= score;
        
        if(Score < 0) Score = 0;
        if(_consecutiveBadValue % 3 == 0 ) GameManager.Instance.CustomerManager.DecreaseCustomerQueue(_consecutiveBadValue);
        
        TrackScore(scoreType);
        GameManager.Instance.InvokeOnScoreChange(Score);
    }
    
    public void AddScore(int score, ScoreType scoreType)
    {
       
        switch (scoreType)
        {
            case ScoreType.perfect:
                ConsecutivePerfectValue++;
                _consecutiveBadValue = 0;
                if(ConsecutivePerfectValue % 3 == 0) GameManager.Instance.CustomerManager.AddCustomerQueue(ConsecutivePerfectValue - 1);
                
                break;
            case ScoreType.wrongCookedness:
                ConsecutivePerfectValue = 0;
                _consecutiveBadValue++;
                break;
        }
        
        Score += score;
        TrackScore(scoreType);
        GameManager.Instance.InvokeOnScoreChange(Score);
    }

    private void TrackScore(ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.perfect:
                PerfectServed++;
                break;
            case ScoreType.wrongCookedness:
                WrongCookednessServed++;
                break;
            case ScoreType.wrongOrder:
                WrongOrderServed++;
                break;
            case ScoreType.unserved:
                UnservedCustomer++;
                break;
        }
        
        if(scoreType != ScoreType.unserved)
            TotalCustomerServed++;
        
        TotalCustomerOrder++;
    }
    
}
