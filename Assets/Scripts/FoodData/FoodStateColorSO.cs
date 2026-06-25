using UnityEngine;

[CreateAssetMenu(fileName = "FoodColorSO", menuName = "ColorSO")]
public class FoodStateColorSO : ScriptableObject
{
    [field: SerializeField] public Color Raw { get; set; }
    [field: SerializeField] public Color Undercooked { get; set; }
    [field: SerializeField] public Color Cooked { get; set; }
    [field: SerializeField] public Color Burnt { get; set; }
}
