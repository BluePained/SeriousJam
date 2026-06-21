using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "FoodItem", menuName = "FoodSO")]
public class FoodSO : ScriptableObject
{
    [field: SerializeField] public string FoodName { get; private set; }
    [field: SerializeField] public FoodSide[] Side { get; private set; }
    [field: SerializeField] public float CookTime { get; private set; }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(Application.isPlaying) return;

        if (Side.Length == 4)
        {
            for (int i = 0; i < Side.Length; i++)
            {
                Side[i].ChangeSide((Side)i);
                Side[i].DefaultValue();
            }
        }
        
        EditorUtility.SetDirty(this);
    }
#endif
}
