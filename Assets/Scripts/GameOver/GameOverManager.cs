using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] public GameOverStatDisplay[] GameOverStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string PlayScene;
    [SerializeField] private TMP_Text TotalScoreText;

    private void Start()
    {
        TotalScoreText.text = GameManager.Instance.ScoreManager.Score.ToString();
    }

    public void UpdateStats()
    {
        foreach (GameOverStatDisplay display in GameOverStats)
        {
            //display.StatType = 
            continue;
        }
    }

    public void LeaveGameOver()
    {
        SceneLoaderManager.Instance.SceneLoad(PlayScene);
        //print("bye");
    }

}
