using UnityEngine;

public class GrillFlameVisualManager : MonoBehaviour
{
    [SerializeField] private GrillManager grillManager;
    [SerializeField] private GameObject TempMeterPointer;
    // add fire particle prefabs in inspector
    [SerializeField] private fireScaleWithHeat[] FlameParticles;


    [SerializeField] private float MinParticleSize = 0.0f;
    [SerializeField] private float MaxParticleSize = 0.5f;

    [SerializeField] private float LerpSpeed = 5;

    private void Start()
    {
        //print(TempMeterPointer.transform.rotation);
        if (grillManager == null)
        {
            print("Grill Manager not set in: " + this.name);
            return;
        }
        
        // setting the min/max Size in the manager on ready
        foreach (fireScaleWithHeat ParticleSystem in FlameParticles)
        {
            ParticleSystem.minSize = MinParticleSize;
            ParticleSystem.maxSize = MaxParticleSize;
        }
    }

    private void Update()
    {
        // Temp Meter rotation lerp
        float HeatPercentage = Mathf.InverseLerp(0f, 5f, grillManager.HeatLevel);
        float TargetAngle = Mathf.Lerp(90f, -90f, HeatPercentage);

        float CurrentAngle = TempMeterPointer.transform.localEulerAngles.z;
        float SmoothAngle = Mathf.LerpAngle(CurrentAngle, TargetAngle, Time.deltaTime * LerpSpeed);
        TempMeterPointer.transform.localRotation = Quaternion.Euler(0f, 0f, SmoothAngle);

        // Flame particle size lerp for each particle
        foreach (fireScaleWithHeat ParticleSystem in FlameParticles)
        {
            float CurrentSize = ParticleSystem.setFireParticleSize;
            float TargetSize = grillManager.HeatLevel * 0.1f;
            ParticleSystem.setFireParticleSize = Mathf.Lerp(CurrentSize, TargetSize, Time.deltaTime * LerpSpeed);
            //ParticleSystem.setFireParticleSize = grillManager.HeatLevel * 0.1f;
        }
    }
}
