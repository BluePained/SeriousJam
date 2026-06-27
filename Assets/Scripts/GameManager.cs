using System;
using Input;
using Unity.Cinemachine;
using UnityEngine;


public enum GameState
{
    None,
    Paused,
    Initializing,
    Playing,
    PrepareToEndGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [field: SerializeField] public Camera MainCamera { get; private set; }
    [field: SerializeField] public CinemachineCamera CinemachineCamera { get; private set; }
    [field: SerializeField] public GameState State { get; private set; }
    [field: SerializeField] public ScoreManager ScoreManager { get; private set; }
    [field: SerializeField] public DifficultyManager DifficultyManager { get; private set; }
    [field: SerializeField] public CustomerManager CustomerManager { get; private set; }

    [field: SerializeField] public string[] TargetScene;
    public event Action<GameState> OnGameStateChange;
    public event Action<int> OnScoreChange;
    public event Action<int> OnQueueChange;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
        
        if(ScoreManager == null) ScoreManager = GetComponent<ScoreManager>();
        if(DifficultyManager == null) DifficultyManager = GetComponent<DifficultyManager>();
        if(CustomerManager == null) CustomerManager = GetComponent<CustomerManager>();
    }

    private void OnGameOver()
    {
        print("GameOver");
        SceneLoaderManager.Instance.AddSceneToLoad(TargetScene);
    }

    public void InvokeOnScoreChange(int score)
    {
        OnScoreChange?.Invoke(score);
    }

    public void InvokeOnQueueChange(int queue)
    {
        OnQueueChange?.Invoke(queue);
    }
 

    public void ChangeState(GameState newState)
    {
        State = newState;
        OnGameStateChange?.Invoke(State);
        print($"Changing State: {State}");
        OnStateChange();
    }

    private void OnStateChange()
    {
        switch (State)
        {
            case GameState.Initializing:
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
                //hmmmmmm i seeee
            case GameState.GameOver:
                OnGameOver();
                break;
        }
    }

}
