using UnityEngine;

[System.Serializable]
public class FoodPattern
{
    [field: SerializeField] public Side FoodSide { get; private set; }
    [field: SerializeField] public Cookedness Cookedness { get; private set; }
}

[System.Serializable]
public class FoodPattenContainer
{
    [field: SerializeField] public FoodPattern[] Patterns { get; private set; }
}

[CreateAssetMenu(fileName = "Food Container", menuName = "Food Container")]
public class FoodContainer : ScriptableObject
{
    [field: SerializeField] public FoodSO[] FoodData { get; private set; }
    [field: SerializeField] public FoodPattenContainer[] ChickenPattern { get; private set; }
    [field: SerializeField] public FoodPattenContainer[] CornPattern { get; private set; }
    [field: SerializeField] public FoodPattenContainer[] FishPattern { get; private set; }
    [field: SerializeField] public FoodPattenContainer[] MeatPattern { get; private set; }
    [field: SerializeField] public FoodPattenContainer[] MushroomPattern { get; private set; }
}
