using UnityEngine;

[CreateAssetMenu(fileName = "FoodSOFourState", menuName = "FoodSO/FoodSOFourState")]
public class FoodSOFourState : FoodSO
{
    [field: SerializeField] public FoodSideDefault[] Side { get; set; }
}
