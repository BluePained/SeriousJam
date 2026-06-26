using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSO", menuName = "CustomerSO")]
public class CustomerSO : ScriptableObject
{
    [field: SerializeField] public Sprite[] HeadType { get; private set; }
    [field: SerializeField] public Sprite[] BlackHair { get; private set; }
    [field: SerializeField] public Sprite[] BlackBackHair { get; private set; }
    [field: SerializeField] public Sprite[] BrownHair { get; private set; }
    [field: SerializeField] public Sprite[] BrownBackHair { get; private set; }
    [field: SerializeField] public Sprite[] Cloth { get; private set; }
    [field: SerializeField] public Sprite[] Mouth { get; private set; }
    [field: SerializeField] public Sprite[] Eyes { get; private set; }
    [field: SerializeField] public Sprite[] Eyebrows { get; private set; }
    [field: SerializeField] public Sprite[] HeadAccessories { get; private set; }
    [field: SerializeField] public Sprite[] ExtraHeadAccessories { get; private set; }
    
    [Header("Preset")]
    [field: SerializeField] public Sprite[] PresetEyebrowsIrritate { get; private set; }
    [field: SerializeField] public Sprite[] PresetMouthIrritate { get; private set; }
    
    [field: SerializeField] public Sprite[] PresetEyebrowsAngry { get; private set; }
    [field: SerializeField] public Sprite[] PresetMouthAngry { get; private set; } 
    
    [field: SerializeField] public Sprite[] PresetEyebrowsSad { get; private set; }
    [field: SerializeField] public Sprite[] PresetMouthSad { get; private set; }
    
    [field: SerializeField] public Sprite[] AngryIcon { get; private set; }
    [field: SerializeField] public Sprite[] DisappointIcon { get; private set; }
}
