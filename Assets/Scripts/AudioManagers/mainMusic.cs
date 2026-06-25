using NUnit.Framework;
using UnityEngine;

public class mainMusic : MonoBehaviour
{
    public static mainMusic instance;

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

        if (!this.GetComponent<AudioSource>().isPlaying)
        {
            this.GetComponent<AudioSource>().Play();
        }
    }
    private void Update()
    {
        if (this.GetComponent<AudioSource>().volume != GlobalSettings.Instance.MainVolume)
        {
            this.GetComponent<AudioSource>().volume = GlobalSettings.Instance.MainVolume;
        }
    }
}
