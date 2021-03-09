//Written By Gabriel Tupy3-8-2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundType : ScriptableObject
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float priority;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;
    [SerializeField] private bool loop;

    private List<AudioSource> activeSources = new List<AudioSource>();

    public AudioClip GetAudioClip()
    {
        return clip;
    }

    private void UpdateAudioSource(AudioSource source)
    {
        source.gameObject.name = this.name;
        source.clip = this.clip;
        source.volume = this.volume;
        source.pitch = this.pitch;
        source.loop = this.loop;
        source.spatialBlend = 0;
    }

    public void Play()
    {
        if (activeSources.Count == 0)
        {
            AudioSource source = AudioSourceManager.GetInstance().GetAudioSource();
            if (source != null)
            {
                UpdateAudioSource(source);
                source.Play();
                activeSources.Add(source);
            }
        }
    }

    public void PlayOneShot()
    {
        AudioSource source = AudioSourceManager.GetInstance().GetAudioSource();
        if (source != null)
        {
            UpdateAudioSource(source);
            source.PlayOneShot(this.clip);
            activeSources.Add(source);
        }
    }

    public void StopSound()
    {
        foreach (AudioSource source in activeSources)
        {
            source.Stop();
            AudioSourceManager.GetInstance().ReturnAudioSource(source);
        }
        activeSources.Clear();
    }
}
