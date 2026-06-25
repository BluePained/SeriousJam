using UnityEngine;

[CreateAssetMenu(fileName = "FoodSOThreeState", menuName = "FoodSO/FoodSOThreeState")]
public class FoodSOThreeState : FoodSO
{
    [field: SerializeField] public FoodSideThreeState[] Side { get; set; }
}
