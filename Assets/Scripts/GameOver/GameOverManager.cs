using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] public GameOverStatDisplay[] GameOverStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void UpdateStats()
    {
        foreach (GameOverStatDisplay display in GameOverStats)
        {
            //display.StatType = 
            continue;
        }
    }

}
