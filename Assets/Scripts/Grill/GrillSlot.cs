using UnityEngine;

public class GrillSlot : PlaceSlot
{
    [SerializeField] private FoodBase placedFood;

    public void AssignFood(FoodBase food)
    {
        placedFood = food;
    }

    public void RemoveFood()
    {
        placedFood = null;
    }

    public void CookTheFood(float heat)
    { 
        placedFood?.CookFood(heat);
    }
}
