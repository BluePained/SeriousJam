using UnityEngine;

public class GrillSlot : MonoBehaviour
{
    [field: SerializeField] public bool IsUsed { get; private set; }

    public void ChangeUsedState(bool state)
    {
        IsUsed = state;
    }
}
