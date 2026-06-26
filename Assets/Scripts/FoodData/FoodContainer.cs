using UnityEngine;

[CreateAssetMenu(fileName = "Food Container", menuName = "Food Container")]
public class FoodContainer : ScriptableObject
{
    [field: SerializeField] public FoodSO[] FoodData { get; private set; }
}
