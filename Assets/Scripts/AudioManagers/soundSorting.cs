using UnityEngine;

[System.Serializable]
public class soundSorting
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pitch = 1;

    public float volumeMinimum = 0.7f;

    public float pitchMinimum = 0.7f;

    public bool isLooping, playAtStart;

    [HideInInspector]
    public AudioSource source;
}
