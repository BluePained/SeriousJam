using System;
using UnityEngine;

public abstract class FoodSO : ScriptableObject
{
    [field: SerializeField] public string FoodName { get; private set; }
    [field: SerializeField] public Sprite FoodSprite { get; private set; }
    [field: SerializeField] public float CookTime { get; private set; }
    
}
