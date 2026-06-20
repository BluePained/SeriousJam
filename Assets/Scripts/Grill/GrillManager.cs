using System;
using UnityEngine;

public class GrillManager : MonoBehaviour
{
    [SerializeField] private Transform[] placeObjectPoint;

#if UNITY_EDITOR
    
    private void OnValidate()
    {
        if(Application.isPlaying) return;
        if(transform.childCount - 1 <= 0) return;
        
        placeObjectPoint = new Transform[transform.childCount - 1];

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            placeObjectPoint[i] = transform.GetChild(i + 1);
        }
        
        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif
    
}
