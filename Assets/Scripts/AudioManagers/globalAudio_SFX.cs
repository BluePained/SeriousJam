using NUnit.Framework;
using System;
using UnityEngine;

public class globalAudio_SFX : MonoBehaviour
{
    public static globalAudio_SFX instance;
    public soundSorting[] sounds;
    public float pitchMin = 0.6f, pitchMax = 1.3f;

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

        foreach (soundSorting s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.isLooping;

            if (s.playAtStart)
            {
                s.source.playOnAwake = true;
            }
            else
            {
                s.source.playOnAwake = false;
            }

        }
    }

    public void Play(string name)
    {
        soundSorting s = Array.Find(sounds, sound => sound.name == name);

        //If you want to limit a sound to one playing at a time per type, enable this return.
        //if (s.source.isPlaying)
        //{
        //return;
        //}

        s.source.volume = s.volumeMinimum * GlobalSettings.Instance.SfxVolume;
        s.source.pitch = UnityEngine.Random.Range(s.pitchMinimum * pitchMin, s.pitchMinimum * pitchMax);
        s.source.Play();
    }

    public void Stop(string name)
    {
        soundSorting s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = s.volumeMinimum * GlobalSettings.Instance.SfxVolume;
        s.source.pitch = UnityEngine.Random.Range(s.pitchMinimum * pitchMin, s.pitchMinimum * pitchMax);
        s.source.Stop();
    }
}
