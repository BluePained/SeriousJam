using UnityEngine;

public class GrillSlot : PlaceSlot
{
    [SerializeField] private FoodInteractableObject placedFood;

    public void AssignFood(FoodInteractableObject food)
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
