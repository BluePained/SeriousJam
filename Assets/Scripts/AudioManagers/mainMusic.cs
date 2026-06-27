using NUnit.Framework;
using UnityEngine;

public class mainMusic : MonoBehaviour
{
    public static mainMusic instance;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        
        audioSource = GetComponent<AudioSource>();

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    private void Update()
    {
        if (audioSource.volume != GlobalSettings.Instance.MainVolume)
        {
            audioSource.volume = GlobalSettings.Instance.MainVolume;
        }
    }
}
