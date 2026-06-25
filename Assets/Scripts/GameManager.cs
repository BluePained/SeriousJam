using System;
using UnityEngine;

public enum GameState
{
    None,
    Paused,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [field: SerializeField] public GameState State { get; private set; }
    
    public event Action<GameState> OnGameStateChange;
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
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        OnGameStateChange?.Invoke(State);
        OnStateChange();
    }

    private void OnStateChange()
    {
        switch (State)
        {
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.GameOver:
                break;
        }
    }

}
