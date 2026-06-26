using UnityEngine;

public class fireScaleWithHeat : MonoBehaviour
{

    //use this float to directly change the flames size. the flames scale value is clamped between minSize and maxSize
    public float setFireParticleSize;
    // setting min/max size in manager so all fire particles can be of uniform scale
    [System.NonSerialized]
    public float minSize, maxSize;

    private void Update()
    {
        if (this.transform.localScale.y != Mathf.Clamp(setFireParticleSize, minSize, maxSize))
        {
            this.transform.localScale = new Vector3(Mathf.Clamp(setFireParticleSize, minSize, maxSize), Mathf.Clamp(setFireParticleSize, minSize, maxSize), Mathf.Clamp(setFireParticleSize, minSize, maxSize));  
        }
    }
}
