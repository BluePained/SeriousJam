using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [field: SerializeField] public int DifficultyValue { get; private set; }
    private float _timer;
    public event Action<int> DifficultyChanged;
    private void Start()
    {
        DifficultyValue = 1;
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 60 && DifficultyValue <= 4)
        {
            DifficultyValue++;
            DifficultyChanged?.Invoke(DifficultyValue);
            _timer = 0;
        }
    }
}
