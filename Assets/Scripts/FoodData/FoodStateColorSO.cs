using UnityEngine;

[CreateAssetMenu(fileName = "FoodColorSO", menuName = "ColorSO")]
public class FoodStateColorSO : ScriptableObject
{
    [field: SerializeField] public Color Raw { get; private set; }
    [field: SerializeField] public Color Undercooked { get; private set; }
    [field: SerializeField] public Color Cooked { get; private set; }
    [field: SerializeField] public Color Burnt { get; private set; }
    [field: SerializeField] public Color Disable { get; private set; }
}
