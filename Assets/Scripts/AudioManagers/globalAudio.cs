using System;
using UnityEngine;

public class globalAudio : MonoBehaviour
{
    public static globalAudio instance;
    public soundSorting[] sounds;
    public float pitchMin = 1, pitchMax = 1;

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
                s.source.Play();
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

        s.source.volume = s.volumeMinimum; //* insert global volume music settings here multipled by s.volumeMinimum
        s.source.pitch = UnityEngine.Random.Range(s.pitchMinimum * pitchMin, s.pitchMinimum * pitchMax);
        s.source.Play();
    }

    public void Stop(string name)
    {
        soundSorting s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = s.volumeMinimum; //* insert global volume music settings here multipled by s.volumeMinimum
        s.source.pitch = UnityEngine.Random.Range(s.pitchMinimum * pitchMin, s.pitchMinimum * pitchMax);
        s.source.Stop();
    }
}
